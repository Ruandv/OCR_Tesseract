using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageManipulations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR_API.Models;

namespace OCR_API.Controllers
{
    [ApiController]
    [Route("api/Ocr")]
    public class OcrController : ControllerBase
    {
        //public IBusControl Bus { get; }
        //public Uri uri = new Uri("rabbitmq://localhost/OCREndpoint_queue");
        //public OcrController()
        //{

        //    //ep = Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/OCREndpoint_queue"));
        //}
        string filePath = "C:/Logs/Images/";
        private string imageName;

        [HttpGet]
        [Route("GetDocuments")]
        public IActionResult Get()
        {
            imageName = "Ruan.pdf";
            byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(filePath, imageName.Replace("pdf", "png")));
            String file = Convert.ToBase64String(bytes);

            return Ok(new string[] { "Get Good", file });
        }

        [HttpPost]
        [Route("ocrDocument/{fileName}")]
        public async Task<IActionResult> OcrDocument(string fileName, Region[] data)
        {
            var ocrImage = new OcrImage(Path.Combine(filePath, fileName));

            var result = await ocrImage.IdentifyData(data.Select(x => new Rectangle((int)x.TopLeft.X, (int)x.TopLeft.Y, (int)x.Width, (int)x.Height)));

            return Ok(result);
        }

        [HttpPost]
        [Route("UploadDocument")]
        public async Task<IActionResult> UploadDocument()
        {
            var httpRequest = HttpContext.Request;
            IFormFileCollection postedFiles = httpRequest.Form.Files;
            //Create custom filename
            List<string> files = new List<string>();
            if (postedFiles.Any())
            {
                foreach (IFormFile postedFile in postedFiles)
                {

                    imageName = (Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).ToString().Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

                    using (var fileStream = new FileStream(Path.Combine(filePath, imageName), FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileStream);
                    }
                    try
                    {
                        var v = new PDF();
                        var sw = Stopwatch.StartNew();
                        v.ConvertPDFToPng(filePath, imageName);
                        sw.Stop();
                        Console.WriteLine("Total time to convert the document is " + sw.Elapsed.TotalSeconds.ToString());
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(filePath, imageName.Replace("pdf", "png")));
                    var file = Convert.ToBase64String(bytes);

                    files.Add(file);
                }
                var res = new { fileName = imageName, file = files };
                return Ok(res);
            }
            return Ok();
        }
    }
}
