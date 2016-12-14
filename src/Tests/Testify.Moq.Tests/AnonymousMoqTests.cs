using Xunit;

namespace Testify.Tests
{
    public class AnonymousMoqTests
    {
        private readonly AnonymousData anon;

        public AnonymousMoqTests()
        {
            this.anon = new AnonymousData();
            this.anon.Customize(new MoqCustomization());
        }

        [Fact]
        public void RegisterMock_Configure_ConfiguresMock()
        {
            bool called = false;
            this.anon.RegisterMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => called = true));
            var test = this.anon.Any<ITestInterface>();

            test.SayHello();

            Assert.True(called);
        }

        [Fact]
        public void FreezeMock_Configure_ConfiguresMock()
        {
            bool called = false;
            this.anon.FreezeMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => called = true));
            var test = this.anon.Any<ITestInterface>();

            test.SayHello();

            Assert.True(called);
        }

        [Fact]
        public void RegisterMock_Any_CreatesNewInstances()
        {
            this.anon.RegisterMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => { }));

            var first = this.anon.Any<ITestInterface>();
            var second = this.anon.Any<ITestInterface>();

            Assert.NotSame(first, second);
        }

        [Fact]
        public void FreezeMock_Any_ReturnsSameInstance()
        {
            this.anon.FreezeMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => { }));

            var first = this.anon.Any<ITestInterface>();
            var second = this.anon.Any<ITestInterface>();

            Assert.Same(first, second);
        }
    }
}