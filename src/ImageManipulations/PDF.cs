using ImageMagick;
using System.IO;

namespace ImageManipulations
{
    public class PDF
    {
        public void ConvertPDFToPng(string pdfPath, string pdfName)
        {
            MagickReadSettings settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(300, 300);

            using (MagickImageCollection images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(File.ReadAllBytes(Path.Combine(pdfPath, pdfName)), settings);

                int page = 1;
                foreach (MagickImage image in images)
                {
                    // Write page to file that contains the page number
                    image.Write(Path.Combine(pdfPath, pdfName.Replace(".pdf", "") + ".png"));
                    // Writing to a specific format works the same as for a single image
                    // image.Format = MagickFormat.Ptif;
                    // image.Write(Path.Combine(pdfPath, pdfName.Replace("pdf", page.ToString()) + ".tif"));
                    page++;
                }
            }
        }
    }
}
