namespace DatabaseLayer
{

    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string code, string name, string identityNumber, string emailAddress)
        {
            Code = code;
            Name = name;
            IdentityNumber = identityNumber;
            EmailAddress = emailAddress;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}