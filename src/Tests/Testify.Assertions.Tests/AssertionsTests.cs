namespace Testify.Assertions.Tests;

using Xunit.Sdk;

public static class AssertionsTests
{
    public class Assert
    {
        [Fact]
        public void SetsTheValue()
        {
            const int value = 42;

            var result = Assert(value);

            XAssert.Equal(42, result.Value);
        }

        [Fact]
        public void SetsTheExpression()
        {
            const int value = 42;

            var result = Assert(value);

            XAssert.Equal("value", result.Expression);
        }
    }

    public class AssertAction
    {
        [Fact]
        public void SetsTheExpression()
        {
            const string expectedMessage = "() => 42";

            var result = Assert(() => 42);

            XAssert.Equal(expectedMessage, result.Expression);
        }
    }

    public class AssertMessageAction
    {
        [Fact]
        public void ShouldSucceedWithNoFailedAssertions()
        {
            var action = Act(() => Assert("Compound assertion.", () => { }));

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldFailWithCompoundMessage()
        {
            const string expectedMessage = """
                Compound assertion.
                   Message 1.
                   Message 2.
                """;

            var action = Act(() =>
                Assert("Compound assertion.", () =>
                {
                    Fail("Message 1.");
                    Fail("Message 2.");
                }));

            ShouldFail(action, expectedMessage);
        }

        [Fact]
        public void ShouldFailWhenNested()
        {
            const string expectedMessage = """
                Outer compound assertion.
                   Inner compound assertion 1.
                      Message 1.
                      Message 2.
                   Inner compound assertion 2.
                      Message 3.
                      Message 4.
                """;

            var action = Act(() =>
                Assert("Outer compound assertion.", () =>
                {
                    Assert("Inner compound assertion 1.", () =>
                    {
                        Fail("Message 1.");
                        Fail("Message 2.");
                    });
                    Assert("Inner compound assertion 2.", () =>
                    {
                        Fail("Message 3.");
                        Fail("Message 4.");
                    });
                }));

            ShouldFail(action, expectedMessage);
        }
    }

    public class ShouldSatisfy
    {
        [Fact]
        public void ShouldSucceedWhenAssertionsSucceed()
        {
            const bool value = true;

            var action = Act(() => Assert(value).ShouldSatisfy(it =>
                {
                    it.ShouldBeTrue();
                    it.ShouldBeTrue();
                }));

            ShouldSucceed(action);
        }

        [Fact]
        public void ShouldFailWithCompoundAssertion()
        {
            const bool value = false;
            var expectedMessage = $"""
                One or more assertions were not satisfied.
                   Expected {FormatExpression("value")} to be true because reason 1, but found false.
                   Expected {FormatExpression("value")} to be true because reason 2, but found false.
                """;

            var action = Act(() =>
                Assert(value).ShouldSatisfy(it =>
                {
                    it.ShouldBeTrue("reason 1");
                    it.ShouldBeTrue("reason 2");
                }));

            ShouldFail(action, expectedMessage);
        }
    }

    public class Fail
    {
        [Fact]
        public void ThrowsFrameworkException()
        {
            var action = Act(() => Fail("Failure message."));

            XAssert.Throws<XunitException>(action);
        }

        [Fact]
        public void SetsExceptionMessage()
        {
            const string expectedMessage = "Failure message.";

            var exception = Record.Exception(() => Fail(expectedMessage));

            XAssert.Equal(expectedMessage, exception.Message);
        }
    }
}
