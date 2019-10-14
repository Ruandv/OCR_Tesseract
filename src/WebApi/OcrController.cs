using IntegrationContracts;
using MassTransit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

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

                var filePath = "C:/Logs/Images/" + imageName;
                postedFile.SaveAs(filePath);

            }
            return Ok("Post Good ");
        }
    }
}