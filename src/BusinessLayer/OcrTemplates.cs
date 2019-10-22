using DatabaseLayer;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class OcrTemplates
    {
        private OcrDatabaseContext db = new OcrDatabaseContext();

        public OcrTemplates()
        {
        }

        public void AddNewTemplate(string templateName, string templateData, byte[] templateImage, string identificationData)
        {
            db.OcrTemplates.Add(new OcrTemplate(templateName, templateData, templateImage, identificationData));
            db.SaveChanges();
        }

        public IEnumerable<OcrTemplate> GetTemplates()
        {
            return db.OcrTemplates.ToArray();
        }

        public OcrTemplate GetTemplate(string description)
        {
            return db.OcrTemplates.First(x => x.TemplateDescription == description);
        }

        public void RemoveAll()
        {
            db.OcrTemplates.RemoveRange(db.OcrTemplates.Where(x => x.Id > 0));
            db.SaveChanges();
        }
    }
}
