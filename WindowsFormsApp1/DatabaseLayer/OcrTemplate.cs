using System;

namespace DatabaseLayer
{
    public class OcrTemplate
    {
        public OcrTemplate()
        {
        }

        public OcrTemplate(string templateDescription, string data)
        {
            TemplateDescription = templateDescription;
            Data = data;
            CreatedTimeStamp = DateTime.Now;
        }

        public int Id { get; set; }
        public string TemplateDescription { get; set; }
        public string Data { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
