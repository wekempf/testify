namespace Testify.Tests;

[Unit]
public class ExceptionAssertionsTests
{
    [Fact]
    public void Throws_ActionThatThrowsExpectedException_DoesNotFail()
    {
        Assert(() => throw new ArgumentNullException("value", "message"))
            .Throws<ArgumentException>(e =>
                VerifyAll(
                    Verify(e.ParamName).IsEqualTo("value"),
                    Verify(e.Message).IsEqualTo("message (Parameter 'value')")));
    }

    [Fact]
    public void Throws_ActionThatThrowsWrongException_Fails()
    {
        try
        {
            Assert(() => throw new ArgumentNullException("value", "message"))
                .Throws<InvalidOperationException>();
        }
        catch (XunitException ex)
        {
            Assert(ex.Message).IsEqualTo("\"throw new ArgumentNullException(\"value\", \"message\")\" was expected to throw System.InvalidOperationException but threw System.ArgumentNullException instead.");
            return;
        }

        Fail("Throws assertion did not fail.");
    }

    [Fact]
    public void Throws_ActionThatDoesNotThrow_Fails()
    {
        try
        {
            Assert(() => Guid.NewGuid())
                .Throws<InvalidOperationException>();
        }
        catch (XunitException ex)
        {
            Assert(ex.Message).IsEqualTo("\"Guid.NewGuid()\" was expected to throw System.InvalidOperationException but did not throw.");
            return;
        }

        Fail("Throws assertion did not fail.");
    }

    [Fact]
    public void Throws_ActionThatThrowsButDoesNotPassAdditionalAssertion_Fails()
    {
        try
        {
            Assert(() => throw new ArgumentNullException("value", "message"))
                .Throws<ArgumentNullException>(e =>
                    VerifyAll(
                        Verify(e.ParamName).IsEqualTo("someOtherValue"),
                        Verify(e.Message).IsEqualTo("some other message")));
        }
        catch (XunitException ex)
        {
            var builder = new StringBuilder();
            builder.AppendLine("\"throw new ArgumentNullException(\"value\", \"message\")\" was expected to throw System.ArgumentNullException, which it did but failed the additional assertion.");
            builder.AppendLine(" * One or more assertions in a compound assertion failed.");
            builder.AppendLine("    * Expected \"e.ParamName\" to be \"someOtherValue\", but found \"value\".");
            builder.Append("    * Expected \"e.Message\" to be \"some other message\", but found \"message (Parameter 'value')\".");
            var pattern = new Regex(@"\r\n|\n\r|\n|\r");
            var expected = pattern.Replace(builder.ToString(), "\n");
            var actual = pattern.Replace(ex.Message, "\n");
            Assert(actual).IsEqualTo(expected);
            return;
        }

        Fail("Throws assertion did not fail.");
    }
}
