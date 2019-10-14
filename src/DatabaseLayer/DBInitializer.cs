using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DatabaseLayer
{
    public class DBInitializer : CreateDatabaseIfNotExists<OcrDatabaseContext>
    {
        protected override void Seed(OcrDatabaseContext context)
        {
            var students = new List<Employee>
            {
            new Employee{DataField2="DeVilliers",DataField1="Ruan",PinCode="1985",EmailAddress = "Ruan.deVilliers@absoluteSys.com"}
            };

            students.ForEach(s => context.Employees.Add(s));

            var templates = new List<OcrTemplate>
            {
            //    new OcrTemplate{TemplateDescription= "Template_24082018130648",Data = "[\"1269, 54, 344, 43\",\"2011, 112, 278, 39\",\"1288, 4, 144, 41\"]",CreatedTimeStamp = DateTime.Now}
            };
            templates.ForEach(s => context.OcrTemplates.Add(s));

            context.SaveChanges();

        }
    }
}
