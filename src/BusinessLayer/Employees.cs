using DatabaseLayer;
using System.Linq;

namespace BusinessLayer
{
    public class Employees
    {
        private OcrDatabaseContext db = new OcrDatabaseContext();

        public Employees()
        {
        }

        public void RemoveEmployees()
        {
            db.Employees.RemoveRange(db.Employees.Where(x => x.Id > 0));
            db.SaveChanges();
        }
        public void AddNewEmployee(string code, string name, string identityNumber, string emailAddress)
        {
            db.Employees.Add(new Employee(code, name, identityNumber, emailAddress));
            db.SaveChanges();
        }

        public Employee GetEmployee(string code, string name)
        {
            return db.Employees.FirstOrDefault(x => x.DataField1 == code && x.DataField2 == name);
        }

    }
}
