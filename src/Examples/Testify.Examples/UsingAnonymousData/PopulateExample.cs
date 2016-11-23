using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.UsingAnonymousData
{
    public class PopulateExample
    {
        [Fact]
        public void Map_ShouldMapFullName()
        {
            var anon = new AnonymousData();
            var dto = anon.Any<EmployeeDto>();
            anon.Populate(dto);
            var mapper = new Mapper();

            var employee = mapper.Map<Employee>(dto);

            Assert(employee.FullName).IsEqualTo($"{dto.LastName}, {dto.FirstName}");
        }
    }
}