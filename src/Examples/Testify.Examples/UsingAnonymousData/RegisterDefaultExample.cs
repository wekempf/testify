using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.UsingAnonymousData
{
    public class RegisterDefaultExample
    {
        // Use whatever the appropriate "assembly initialize" logic your
        //
        static RegisterDefaultExample()
        {
            AnonymousData.RegisterDefault<Employee>(
                f => new Employee(f.AnyFirstName(), f.AnySurname()));
        }

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