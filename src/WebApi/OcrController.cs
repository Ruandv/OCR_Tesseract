using IntegrationContracts;
using MassTransit;
using System;
using System.Web.Http;

namespace WebApi
{
    [RoutePrefix("api/Ocr")]
    public class OcrController : ApiController
    {
        public IBus Bus { get; }

        public OcrController(IBusControl bus)
        {
            Bus = bus;
            Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/OCREndpoint_queue"));
        }


        [HttpGet]
        [Route("GetDocuments")]
        public IHttpActionResult Get()
        {

            return Ok(new string[] { "Get Good", DateTime.UtcNow.ToString() });
        }

        [HttpPost]
        [Route("UploadDocument")]
        public IHttpActionResult UploadDocument(UploadDocumentInfo documentPath)
        {
            Bus.Send(new UploadPdfDocument()
            {
                DocumentLocation = documentPath.DocumentPath
            });
            return Ok("Post Good " + documentPath);
        }
    }
}