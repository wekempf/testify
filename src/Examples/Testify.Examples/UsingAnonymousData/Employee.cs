namespace Examples.UsingAnonymousData
{
    public class Employee
    {
        public Employee(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string FullName => $"{this.LastName}, {this.FirstName}";
    }
}