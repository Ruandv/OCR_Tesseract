using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using Tesseract;

namespace BusinessLayer
{
    public interface ISmtpInfo
    {
        void Send(string toEmailAddress, string subject, string body, byte[] protectedDocument);
    }

    public class OcrImage
    {
        byte[] buffer = new byte[16 * 1024];

        private byte[] myPdfImage;
        private Image pngImage;
        private byte[] protectedDocument;
        private ISmtpInfo smtp;

        public OcrImage(string pdfImagePath, ISmtpInfo smtp)
        {
            this.smtp = smtp;
            SetBasics(pdfImagePath);
        }

        public OcrImage(string pdfImagePath)
        {
            SetBasics(pdfImagePath);
        }

        private void SetBasics(string pdfImagePath)
        {
            try
            {
                myPdfImage = File.ReadAllBytes(pdfImagePath);
                ConvertPdfToPng(myPdfImage);
                if (ConfigurationManager.AppSettings.Get("DeleteUnencryptedFiles").ToUpper() == "TRUE")
                    File.Delete(pdfImagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ConvertPdfToPng(byte[] pdfImage)
        {
            var ms = new MemoryStream();
            new MemoryStream(pdfImage).CopyTo(ms);
            Spire.Pdf.PdfDocument d = new Spire.Pdf.PdfDocument(ms);
            pngImage = d.SaveAsImage(0, 300, 300);
            d.Close();
        }

        public Image GetImage()
        {
            return pngImage;
        }

        public void EncryptFile(string password)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfReader reader = new PdfReader(myPdfImage);
                PdfStamper stamper = new PdfStamper(reader, ms);
                byte[] USER = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] OWNER = System.Text.Encoding.ASCII.GetBytes(password);
                stamper.SetEncryption(USER, OWNER, PdfWriter.AllowPrinting, PdfWriter.ENCRYPTION_AES_128);
                stamper.Close();
                reader.Close();
                ms.Flush();
                protectedDocument = ms.GetBuffer();
            }
        }

        public string SaveFile(string fileName)
        {
            string path = ConfigurationManager.AppSettings.Get("EncryptionDirectory");

            if (!path.EndsWith(@"\")) path += @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(Path.Combine(path, fileName)))
            {
                File.Delete(Path.Combine(path, fileName));
            }

            if (protectedDocument != null)
                File.WriteAllBytes(Path.Combine(path, fileName) + ".pdf", protectedDocument);
            else
                File.WriteAllBytes(Path.Combine(path, fileName) + ".pdf", myPdfImage);

            return Path.Combine(path, fileName) + ".pdf";
        }

        public void SaveFileAsError(string fileName)
        {
            string path = ConfigurationManager.AppSettings["ErrorDirectory"];

            if (!path.EndsWith(@"\")) path += @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(Path.Combine(path, fileName)))
            {
                File.Delete(Path.Combine(path, fileName));
            }
            File.WriteAllBytes(Path.Combine(path, fileName) + ".pdf", myPdfImage);
        }

        public void Send(string emailAddress)
        {
            if (smtp == null)
            {
                throw new NotSupportedException("Please supply a class that implements the ISmtpInfo Interface");
            }

            var document = protectedDocument;

            if (protectedDocument == null)
                document = myPdfImage;

            smtp.Send(emailAddress, ConfigurationManager.AppSettings["EmailSubject"], ConfigurationManager.AppSettings["EmailMessage"], document);
        }

        public IEnumerable<string> IdentifyData(IEnumerable<Rectangle> coOrdinates)
        {
            List<string> data = new List<string>();
            try
            {
                var img = new Bitmap(pngImage);
                img.SetResolution(300, 300);
                foreach (Rectangle rec in coOrdinates)
                {
                    var ocr = new TesseractEngine(ConfigurationManager.AppSettings.Get("TessarecDirectory"), "eng", EngineMode.Default);
                    var page = ocr.Process(img, new Rect(rec.X, rec.Y, rec.Width, rec.Height));
                    data.Add(page.GetText().Trim());
                    ocr.Dispose();
                }
                img.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
    }
}