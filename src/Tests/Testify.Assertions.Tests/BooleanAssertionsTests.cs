namespace Testify.Tests;

public static class BooleanAssertionsTests
{
    public class ShouldBeTrue
    {
        [Fact]
        [Bug(12345)]
        public void ShouldSucceedWhenTrue()
        {
            const bool value = true;

            var action = Act(() => Assert(value).ShouldBeTrue());

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldFailWhenFalse()
        {
            const bool value = false;
            const string because = "I say so";

            var action = Act(() => Assert(value).ShouldBeTrue(because));

            ShouldFail(action, $"Expected {FormatExpression("value")} to be true because I say so, but found false.");
        }
    }

    public class ShouldBeFalse
    {
        [Fact]
        public void ShouldSucceedWhenFalse()
        {
            const bool value = false;

            var action = Act(() => Assert(value).ShouldBeFalse());

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldFailWhenTrue()
        {
            const bool value = true;
            const string because = "I say so";

            var action = Act(() => Assert(value).ShouldBeFalse(because));

            ShouldFail(action, $"Expected {FormatExpression("value")} to be false because I say so, but found true.");
        }
    }
}
