using System;

namespace OCR.Database.Layer
{
    public class OcrTemplate
    {

        public OcrTemplate()
        {
        }

        public OcrTemplate(string templateDescription, string data, byte[] templateImage, string identificationData)
        {
            TemplateDescription = templateDescription;
            IdentificationData = identificationData;
            Data = data;
            TemplateImage = templateImage;
            CreatedTimeStamp = DateTime.Now;
        }

        public int Id { get; set; }
        public string TemplateDescription { get; set; }
        public string IdentificationData { get; set; }
        public string Data { get; set; }
        public byte[] TemplateImage { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
