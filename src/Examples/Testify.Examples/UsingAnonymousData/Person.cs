namespace Examples.UsingAnonymousData
{
    public class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string FullName => $"{LastName}, {FirstName}";

        public string LastName { get; }
    }
}