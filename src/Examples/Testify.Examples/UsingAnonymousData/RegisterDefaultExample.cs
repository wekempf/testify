using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.UsingAnonymousData
{
    public class RegisterDefaultExample
    {
        // Use whatever the appropriate "assembly initialize" logic your
        // unit test framework uses.
        static RegisterDefaultExample() =>
            AnonymousData.RegisterDefault<Employee>(
                anon => new Employee(anon.AnyFirstName(), anon.AnySurname(), anon.AnyBool()));

        [Fact]
        public void FullName_ShouldBeCalculated()
        {
            var anon = new AnonymousData();
            var employee = anon.Any<Employee>();

            var fullName = employee.FullName;

            Assert(fullName).IsEqualTo($"{employee.LastName}, {employee.FirstName}");
        }
    }
}