using System;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class AssertionsTests
    {
        [Fact]
        public void Assert_ReturnsActualValue()
        {
            var actualValue = new object();

            var result = Assert(actualValue);

            if (!object.ReferenceEquals(result.Value, actualValue))
            {
                Fail("Actual value not set correctly.");
            }
        }

        [Fact]
        public void CompoundAssertion_Errors_ShouldReportErrors()
        {
            try
            {
                AssertAll(
                    "An error occurred.",
                    () => Fail("First"),
                    () => Fail("Second"));
            }
            catch (AssertionException e)
            {
                if (e.InnerExceptions.Count != 2)
                {
                    Fail("CompoundAssertion didn't report the correct number of errors.");
                }

                return;
            }

            Fail("CompoundAssert didn't report any errors.");
        }

        [Fact]
        public void CompoundAssertion_NoErrors_ShouldNotThrow()
        {
            try
            {
                AssertAll(
                    "An error occurred.",
                    () => { },
                    () => { });
            }
            catch (AssertionException)
            {
                Fail("CompoundAssertion threw when it shouldn't have.");
            }
        }

        [Fact]
        public void Fail_ShouldThrowException()
        {
            try
            {
                Fail("An error occurred.");
            }
            catch (AssertionException e)
            {
                if (e.Message != "An error occurred.")
                {
                    throw new AssertionException("Message not set correctly.");
                }

                return;
            }

            throw new AssertionException("Fail did not throw.");
        }

        [Fact]
        public void Fail_WithParameters_ShouldFormatMessage()
        {
            try
            {
                Fail("An {0} occurred.", "error");
            }
            catch (AssertionException e)
            {
                if (e.Message != "An error occurred.")
                {
                    throw new AssertionException("Message not set correctly.");
                }

                return;
            }

            throw new AssertionException("Fail did not throw.");
        }

        [Fact]
        public void ReplaceNullChars_StringWithNulls_ShouldReplaceNulls()
        {
            const string text = "xxx\0xxx";

            var result = ReplaceNullChars(text);

            Assert(result).Equals("xxx\\0xxx");
        }

        [Fact]
        public void Throw_ThrowsException()
        {
            var inner = new[] { new Exception("first"), new Exception("second") };
            Assert(() => Throw("xyzzy", inner, "Assertion message.", "Some {0}.", "message"))
                .Throws<AssertionException>(
                    e =>
                    {
                        AssertAll(
                            "Unexpected exception state.",
                            () => Assert(e.Message).IsEqualTo("Some message. xyzzy failed. Assertion message. (first) (second)"),
                            () => Assert(e.InnerExceptions).IsSequenceEqualTo(inner));
                    });
        }
    }
}