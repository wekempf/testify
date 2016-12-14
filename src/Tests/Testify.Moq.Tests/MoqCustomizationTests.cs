using Xunit;

namespace Testify.Tests
{
    public class MoqCustomizationTests
    {
        private readonly AnonymousData anon;

        public MoqCustomizationTests()
        {
            this.anon = new AnonymousData();
            this.anon.Customize(new MoqCustomization());
        }

        [Fact]
        public void Any_ITestInterface_ShouldCreateMock()
        {
            var result = this.anon.Any<ITestInterface>();

            Assert.NotNull(result);
        }
    }
}