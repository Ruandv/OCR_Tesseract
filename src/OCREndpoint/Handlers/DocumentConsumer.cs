using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using BusinessLayer;
using IntegrationContracts;
using IntegrationContracts.Events;
using iTextSharp.text.pdf;
using MassTransit;

namespace OCREndpoint.Handlers
{
    public class DocumentConsumer : IConsumer<PdfDocumentUploaded>,
        IConsumer<UploadPdfDocument>,
        IConsumer<DocumentPageExtracted>
    {
        //private readonly IBus bus;
         public OcrPage OcrPage { get; }

        public DocumentConsumer()
        {
            //this.bus = new IBus();
            OcrPage = new OcrPage();
        }

        public async Task Consume(ConsumeContext<PdfDocumentUploaded> context)
        {
            var reader = new PdfReader(context.Message.DocumentLocation);
            int nop = reader.NumberOfPages;
            reader.Dispose();
            for (int i = 1; i <= nop; i++)
            {
                ExtractPages(context.Message.DocumentLocation, Path.Combine(ConfigurationManager.AppSettings.Get("ApplicationDirectory"), Guid.NewGuid().ToString()) + ".pdf", i, 1);
            }
        }

        public async Task Consume(ConsumeContext<UploadPdfDocument> context)
        {
            Console.Write("BLAHHH");
        }

        public async Task Consume(ConsumeContext<DocumentPageExtracted> context)
        {
            //Store the document to the DB.
            OcrPage.SavePage(context.Message.PageName, context.Message.Page);
        }

        private void ExtractPages(string sourcePDFpath, string outputPDFpath, int startpage, int endpage)
        {
            Console.WriteLine("Extracting document " + DateTime.Now.Millisecond);
            PdfReader reader = null;
            iTextSharp.text.Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            PdfReader.unethicalreading = true;
            reader = new PdfReader(sourcePDFpath);

            sourceDocument = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(startpage));
            pdfCopyProvider = new PdfCopy(sourceDocument, new FileStream(outputPDFpath, FileMode.Create));
            sourceDocument.Open();

            for (int i = startpage; i < startpage + endpage; i++)
            {
                try
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
            }
            sourceDocument.Close();
            reader.Close();
            var bytes = File.ReadAllBytes(outputPDFpath);
            //bus.Publish(new DocumentPageExtracted(outputPDFpath, Guid.NewGuid().ToString(), bytes));
        }

    }
}