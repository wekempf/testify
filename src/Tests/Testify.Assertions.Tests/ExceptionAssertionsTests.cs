namespace Testify.Assertions.Tests;

public static class ExceptionAssertionsTests
{
    public class ShouldThrow
    {
        [Fact]
        public void ShouldSucceedWhenActionThrows()
        {
            var action = Act(() =>
                Assert(() => throw new InvalidOperationException())
                    .ShouldThrow<InvalidOperationException>());

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldSucceedWhenActionThrowsDerivedException()
        {
            var action = Act(() =>
                Assert(() => throw new ArgumentNullException())
                    .ShouldThrow<ArgumentException>());

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldFailWhenActionDoesNotThrow()
        {
            var expectedMessage = $"Expected {FormatExpression("() => { }")} to throw an exception of type System.Exception because some reason, but it did not throw.";

            var action = Act(() =>
                Assert(() => { })
                    .ShouldThrow("some reason"));

            ShouldFail(action, expectedMessage);
        }

        [Fact]
        public void ShouldFailWhenWrongExceptionThrown()
        {
            var expectedMessage = $"Expected {FormatExpression("() => throw new InvalidOperationException()")} to throw an exception of type System.ArgumentException because some reason, but it threw an exception of type System.InvalidOperationException instead.";

            var action = Act(() =>
                Assert(() => throw new InvalidOperationException())
                    .ShouldThrow<ArgumentException>("some reason"));

            ShouldFail(action, expectedMessage);
        }
    }
}
