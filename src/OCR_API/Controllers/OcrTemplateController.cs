using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ImageManipulations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OCR.Database.Layer;
using OCR_API.Configurations;
using OCR_API.Models;

namespace OCR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrTemplateController : ControllerBase
    {
        private OcrTemplateConfiguration configSettings;
        private OCRContext dbContext;
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public OcrTemplateController(IOptions<OcrTemplateConfiguration> configSettings, OCRContext ctx)
        {
            this.configSettings = configSettings.Value;
            this.dbContext = ctx;
        }

        [HttpGet]
        [Route("GetTemplates")]
        public IActionResult GetTemplates()
        {
            var data = dbContext.Templates.ToArray();
            return Ok(data.Select(x => new { x.TemplateDescription, IdentificationData = JsonSerializer.Deserialize<Region[]>(x.IdentificationData, options) }));
        }

        [HttpGet]
        [Route("{templateName}")]
        public IActionResult Get(string templateName)
        {
            var imageName = templateName;
            byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(configSettings.StorageLocation, imageName.Replace("pdf", "png")));
            string file = Convert.ToBase64String(bytes);

            return Ok(new string[] { file });
        }

        [HttpPost]
        [Route("{templateName}/Save")]
        public async Task<IActionResult> SaveTemplate(string templateName, Region[] regions)
        {
            var imageName = templateName;
            var data = dbContext.Templates.FirstOrDefault(x => x.TemplateDescription == imageName);
            if (data == null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(configSettings.StorageLocation, imageName.Replace("pdf", "png")));
                dbContext.Add(new OcrTemplate(templateName, "Some Data", bytes, JsonSerializer.Serialize(regions, options)));
            }
            else
            {
                data.IdentificationData = JsonSerializer.Serialize(regions, options);
            }
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{templateName}")]
        public async Task<IActionResult> DeleteTemplate(string templateName)
        {
            var imageName = templateName;
            var data = dbContext.Templates.FirstOrDefault(x => x.TemplateDescription == imageName);
            dbContext.Templates.Remove(data);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("{TemplateName}/UploadDocument")]
        public async Task<IActionResult> UploadDocument(string templateName)
        {

            var httpRequest = HttpContext.Request;
            IFormFileCollection postedFiles = httpRequest.Form.Files;
            //Create custom filename
            List<string> files = new List<string>();
            string imageName = "";

            if (postedFiles.Any())
            {
                if (postedFiles.Count > 1)
                {
                    throw new InvalidDataException("Only a single page per Template is allowed");
                }
                foreach (IFormFile postedFile in postedFiles)
                {
                    imageName = templateName + Path.GetExtension(postedFile.FileName);
                    if (!Directory.Exists(configSettings.StorageLocation))
                    {
                        Directory.CreateDirectory(configSettings.StorageLocation);
                    }
                    using (var fileStream = new FileStream(Path.Combine(configSettings.StorageLocation, imageName), FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileStream);
                    }
                    try
                    {
                        var v = new PDF();
                        var sw = Stopwatch.StartNew();
                        v.ConvertPDFToPng(configSettings.StorageLocation, imageName);
                        sw.Stop();
                        Console.WriteLine("Total time to convert the document is " + sw.Elapsed.TotalSeconds.ToString());
                        byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(configSettings.StorageLocation, imageName.Replace("pdf", "png")));
                        var file = Convert.ToBase64String(bytes);
                        files.Add(file);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                var res = new { fileName = imageName, file = files };
                return Ok(res);
            }
            return Ok();
        }


    }
}