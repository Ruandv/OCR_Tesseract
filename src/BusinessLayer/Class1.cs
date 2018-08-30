using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OcrTemplates
    {
        private DatabaseContext db = new DatabaseContext();

        public OcrTemplates()
        {
        }

        public void AddNewTemplate(string templateName,string templateData)
        {
            db.OcrTemplates.Add(new OcrTemplate(templateName, templateData));
            db.SaveChanges();
        }


        public IEnumerable<OcrTemplate> GetTemplates()
        {
            return db.OcrTemplates.ToArray();
        }
    }
}
