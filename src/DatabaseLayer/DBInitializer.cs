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
            new Employee{Name="Mr R De Villiers",Code="VIL003",IdentityNumber="8508175060080",EmailAddress = "Ruan.deVilliers@absoluteSys.com"},
            new Employee{Name="Meredith",Code="Alonso",IdentityNumber="5634773542510",EmailAddress = "5634773542510@absoluteSys.com"},
            new Employee{Name="Arturo",Code="Anand",IdentityNumber="0420528118325",EmailAddress = "0420528118325@absoluteSys.com"},
            new Employee{Name="Gytis",Code="Barzdukas",IdentityNumber="5418339800446",EmailAddress = "5418339800446@absoluteSys.com"},
            new Employee{Name="Yan",Code="Li",IdentityNumber="0308232801744",EmailAddress = "0308232801744@absoluteSys.com"},
            new Employee{Name="Peggy",Code="Justice",IdentityNumber="5104611364884",EmailAddress = "5104611364884@absoluteSys.com"},
            new Employee{Name="Laura",Code="Norman",IdentityNumber="4048948024104",EmailAddress = "4048948024104@absoluteSys.com"},
            new Employee{Name="Nino",Code="Olivetto",IdentityNumber="8821845462115",EmailAddress = "8821845462115@absoluteSys.com"}
            };

            students.ForEach(s => context.Employees.Add(s));

            var templates = new List<OcrTemplate>
            {
                new OcrTemplate{TemplateDescription= "Template_24082018130648",Data = "[\"1269, 54, 344, 43\",\"2011, 112, 278, 39\",\"1288, 4, 144, 41\"]",CreatedTimeStamp = DateTime.Now}
            };
            templates.ForEach(s => context.OcrTemplates.Add(s));

            context.SaveChanges();

        }
    }
}
