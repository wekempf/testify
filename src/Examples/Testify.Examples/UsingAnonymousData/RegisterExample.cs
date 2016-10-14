using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.UsingAnonymousData
{
    public class RegisterExample
    {
        [Fact]
        public void FullName_ShouldBeCalculated()
        {
            var anon = new AnonymousData();
            anon.Register<Person>(f => new Person(f.AnyFirstName(), f.AnySurname()));
            var person = anon.Any<Person>();

            var fullName = person.FullName;

            Assert(fullName).IsEqualTo($"{person.LastName}, {person.FirstName}");
        }
    }
}