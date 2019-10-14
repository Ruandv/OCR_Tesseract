namespace DatabaseLayer
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string dataField1, string dataField2, string pinCode,string emailAddress)
        {
            DataField1 = dataField1;
            DataField2 = dataField2;
            PinCode = pinCode;
            EmailAddress = emailAddress;
        }

        public int Id { get; set; }
        public string DataField1 { get; set; }
        public string DataField2 { get; set; }
        public string PinCode { get; set; }
        public string EmailAddress { get; set; }
    }
}