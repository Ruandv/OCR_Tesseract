using IntegrationContracts;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    [RoutePrefix("api/Ocr")]
    public class OcrController : ApiController
    {
        public IBusControl Bus { get; }
        public Uri uri = new Uri("rabbitmq://localhost/OCREndpoint_queue");
        public OcrController(IBusControl bus)
        {
            Bus = bus;
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
        public async Task<IHttpActionResult> UploadDocument(UploadDocumentInfo documentPath)
        {
            var requestTimeout = TimeSpan.FromSeconds(30);
            var document = new UploadPdfDocument()
            {
                DocumentLocation = documentPath.DocumentPath
            };

            IRequestClient<UploadPdfDocument, PdfDocumentUploaded> c = new MessageRequestClient<UploadPdfDocument, PdfDocumentUploaded>(Bus, uri, requestTimeout);
            try
            {
                var a = await c.Request(document);
            }
            catch (Exception)
            {

                throw;
            }
             
            return Ok("Post Good " );
        }
    }
}