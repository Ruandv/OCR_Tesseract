using DatabaseLayer;
using System.Collections.Generic;
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

        public IList<Employee> GetEmployeeList()
        {
            return db.Employees.Where(x => x.Id > 0).ToList();
        }

        public Employee GetEmployee(string code, string name)
        {
            return db.Employees.FirstOrDefault(x => x.DataField1 == code && x.DataField2 == name);
        }

        public Employee GetEmployeeById(int value)
        {
            return db.Employees.Find(value);
        }

        public void Update(Employee emp)
        {

            var c = db.Employees.Find(emp.Id);
            c = emp;
            db.SaveChanges();
        }
    }
}
