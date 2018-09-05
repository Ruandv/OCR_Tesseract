using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using IntegrationContracts;
using iTextSharp.text.pdf;
using MassTransit;

namespace OCREndpoint.Handlers
{
    public class DocumentConsumer : IConsumer<PdfDocumentUploaded>,
        IConsumer<UploadPdfDocument>
    {
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
                //Application.DoEvents();
                try
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }
                catch (System.ArgumentException ex)
                {
                    throw ex;
                }
            }
            sourceDocument.Close();
            reader.Close();
        }

    }
}