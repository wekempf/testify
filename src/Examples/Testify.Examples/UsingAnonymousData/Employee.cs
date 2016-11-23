namespace Examples.UsingAnonymousData
{
    public class Employee
    {
        public Employee(string firstName, string lastName, bool isManager)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsManager = isManager;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string FullName => $"{this.LastName}, {this.FirstName}";

        public bool IsManager { get; }
    }
}