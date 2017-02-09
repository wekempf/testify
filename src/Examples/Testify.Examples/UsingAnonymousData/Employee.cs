namespace Examples.UsingAnonymousData
{
    public class Employee
    {
        public Employee(string firstName, string lastName, bool isManager)
        {
            FirstName = firstName;
            LastName = lastName;
            IsManager = isManager;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string FullName => $"{LastName}, {FirstName}";

        public bool IsManager { get; }
    }
}