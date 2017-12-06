using Xunit;

namespace Testify.Tests
{
    public class AnonymousMoqTests
    {
        private readonly AnonymousData anon;

        public AnonymousMoqTests()
        {
            anon = new AnonymousData();
            anon.Customize(new MoqCustomization());
        }

        [Fact]
        public void FreezeMock_Any_ReturnsSameInstance()
        {
            anon.FreezeMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => { }));

            var first = anon.Any<ITestInterface>();
            var second = anon.Any<ITestInterface>();

            Assert.Same(first, second);
        }

        [Fact]
        public void FreezeMock_Configure_ConfiguresMock()
        {
            var called = false;
            anon.FreezeMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => called = true));
            var test = anon.Any<ITestInterface>();

            test.SayHello();

            Assert.True(called);
        }

        [Fact]
        public void RegisterMock_Any_CreatesNewInstances()
        {
            anon.RegisterMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => { }));

            var first = anon.Any<ITestInterface>();
            var second = anon.Any<ITestInterface>();

            Assert.NotSame(first, second);
        }

        [Fact]
        public void RegisterMock_Configure_ConfiguresMock()
        {
            var called = false;
            anon.RegisterMock<ITestInterface>(mock => mock.Setup(t => t.SayHello()).Callback(() => called = true));
            var test = anon.Any<ITestInterface>();

            test.SayHello();

            Assert.True(called);
        }
    }
}