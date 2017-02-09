using Xunit;

namespace Testify.Tests
{
    public class MoqCustomizationTests
    {
        private readonly AnonymousData anon;

        public MoqCustomizationTests()
        {
            anon = new AnonymousData();
            anon.Customize(new MoqCustomization());
        }

        [Fact]
        public void Any_ITestInterface_ShouldCreateMock()
        {
            var result = anon.Any<ITestInterface>();

            Assert.NotNull(result);
        }
    }
}