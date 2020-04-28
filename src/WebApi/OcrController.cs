using MassTransit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi
{
    [RoutePrefix("api/Ocr")]
    public class OcrController : ApiController
    {
        public IBusControl Bus { get; }
        public Uri uri = new Uri("rabbitmq://localhost/OCREndpoint_queue");
        public OcrController()
        {

            //ep = Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/OCREndpoint_queue"));
        }


        [HttpGet]
        [Route("GetDocuments")]
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "Get Good", DateTime.UtcNow.ToString() });
        }

        [HttpPost]
        [Route("UploadDocument")]
        public async Task<IHttpActionResult> UploadDocument()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["File"];
            //Create custom filename
            if (postedFile != null)
            {
                var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

                var filePath = "C:/Logs/Images/";
                postedFile.SaveAs(Path.Combine(filePath, imageName));
                try
                {
                    var v = new PDF();
                    v.ConvertPDFToPng(filePath, imageName);
                }
                catch (Exception ex)
                {

                    throw;
                }
                Byte[] bytes = File.ReadAllBytes(Path.Combine(filePath, imageName.Replace("pdf","png")));
                String file = Convert.ToBase64String(bytes);
                Console.Write(file);
                return Ok(file);
            }
            return Ok();
        }
    }
}