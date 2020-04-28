using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Tesseract;

namespace OCR_API.Models
{
    public class OcrImage
    {

        private byte[] myPdfImage;
        private string pdfImagePath;
        string path;

        public OcrImage(string pdfImagePath)
        {
            this.pdfImagePath = pdfImagePath;
            path = pdfImagePath.Substring(0, pdfImagePath.LastIndexOf('/'));
            SetBasics(pdfImagePath);
        }

        private void SetBasics(string pdfImagePath)
        {
            try
            {
                myPdfImage = File.ReadAllBytes(pdfImagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string SaveFile(string fileName)
        {

            if (!path.EndsWith(@"\")) path += @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(Path.Combine(path, fileName)))
            {
                File.Delete(Path.Combine(path, fileName));
            }
            return Path.Combine(path, fileName) + ".pdf";
        }

        public void SaveFileAsError(string fileName)
        {
            if (!path.EndsWith(@"\")) path += @"\";
            if (!Directory.Exists(path + @"ERRORS\"))
                Directory.CreateDirectory(path);

            if (File.Exists(Path.Combine(path, "Errors", fileName)))
            {
                File.Delete(Path.Combine(path, "Errors", fileName));
            }
            File.WriteAllBytes(Path.Combine(path, "Errors", fileName) + ".pdf", myPdfImage);
        }

        public async Task<IEnumerable<string>> IdentifyData(IEnumerable<Rectangle> coOrdinates)
        {
            return await Task.Run(() =>
            {
                List<string> data = new List<string>();
                var tessarecDirectory = @"C:\Program Files (x86)\Tesseract-OCR\tessdata";
                try
                {
                    using (var imgs = Pix.LoadFromFile(Path.Combine(path, pdfImagePath.Replace(".pdf", ".png"))))
                    {
                        foreach (Rectangle rec in coOrdinates)
                        {
                            var engine = new TesseractEngine(tessarecDirectory, "eng", EngineMode.Default);
                            var page = engine.Process(imgs, new Rect(rec.X, rec.Y, Math.Abs(rec.Width), Math.Abs(rec.Height)));
                            var txt = page.GetText().Trim();
                            data.Add(txt);
                            engine.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return data;
            });
        }
    }
}
