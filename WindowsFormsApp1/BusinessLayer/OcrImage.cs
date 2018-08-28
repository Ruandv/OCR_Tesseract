﻿using iTextSharp.text.pdf;
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
        void Send(byte[] protectedDocument);
    }

    public class OcrImage
    {
        byte[] buffer = new byte[16 * 1024];
        private string PdfImagePath;
        private byte[] MyPdfImage;
        private Image pngImage;
        private Guid MyGuid;
        private byte[] ProtectedDocument;
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
            MyGuid = Guid.NewGuid();
            PdfImagePath = pdfImagePath;
            MyPdfImage = File.ReadAllBytes(pdfImagePath);
            ConvertPdfToPng(MyPdfImage);
            if (ConfigurationManager.AppSettings.Get("DeleteUnencryptedFiles").ToUpper() == "TRUE")
                File.Delete(pdfImagePath);
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
                PdfReader reader = new PdfReader(MyPdfImage);
                PdfStamper stamper = new PdfStamper(reader, ms);
                byte[] USER = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] OWNER = System.Text.Encoding.ASCII.GetBytes(password);
                stamper.SetEncryption(USER, OWNER, PdfWriter.AllowPrinting, PdfWriter.ENCRYPTION_AES_128);
                stamper.Close();
                reader.Close();
                ms.Flush();
                ProtectedDocument = ms.GetBuffer();
            }
        }

        public void SaveFile(string fileName)
        {
            string path = ConfigurationManager.AppSettings.Get("EncryptionDirectory");

            if (!path.EndsWith(@"\")) path += @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(Path.Combine(path, fileName)))
            {
                File.Delete(Path.Combine(path, fileName));
            }
            File.WriteAllBytes(Path.Combine(path, fileName) + ".pdf", ProtectedDocument);
        }
        public void Send()
        {
            if (smtp == null)
            {
                throw new NotSupportedException("Please supply a class that implements the ISmtpInfo Interface");
            }
            smtp.Send(ProtectedDocument);
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
                    var ocr = new TesseractEngine(ConfigurationManager.AppSettings.Get("TessarecDirectory"), "eng", EngineMode.TesseractAndCube);
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