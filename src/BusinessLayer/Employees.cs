using DatabaseLayer;
using System.Linq;

namespace BusinessLayer
{
    public class OcrPage
    {
        private OcrDatabaseContext db = new OcrDatabaseContext();
        public void SavePage(string pageName, byte[] page)
        {
            var pg = new OcrDocumentPage();
            pg.Page = page;
            pg.PageName = pageName;
            db.Page.Add(pg);
            db.SaveChanges();
        }
    }

    public class Employees
    {
        private OcrDatabaseContext db = new OcrDatabaseContext();

        public Employees()
        {
        }

        public void AddNewEmployee(string code, string name, string identityNumber, string emailAddress)
        {
            db.Employees.Add(new Employee(code, name, identityNumber, emailAddress));
            db.SaveChanges();
        }

        public Employee GetEmployee(string code, string name)
        {
            return db.Employees.FirstOrDefault(x => x.Code == code && x.Name == name);
        }

    }
}
