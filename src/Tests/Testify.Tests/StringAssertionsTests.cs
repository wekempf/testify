using System.Text.RegularExpressions;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class StringAssertionsTests
    {
        [Fact]
        public void Contains_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foobar").Contains("baz", "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed. String 'foobar' does not contain string 'baz'.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_GivenMessageParameters_ShouldDisplayMessage()
        {
            try
            {
                Assert("foobar").Contains("baz", "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed. String 'foobar' does not contain string 'baz'.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_NonSubstring_ShouldThrow()
        {
            try
            {
                Assert("foobar").Contains("baz");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Contains failed. String 'foobar' does not contain string 'baz'.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_Substring_ShouldNotThrow() => Assert("foobar").Contains("foo");

        [Fact]
        public void DoesNotMatch_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").DoesNotMatch(new Regex("foo"), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotMatch failed. String 'foo' matches pattern 'foo'.");
                return;
            }

            Fail("DoesNotMatch did not throw.");
        }

        [Fact]
        public void DoesNotMatch_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").DoesNotMatch(new Regex("foo"), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotMatch failed. String 'foo' matches pattern 'foo'.");
                return;
            }

            Fail("DoesNotMatch did not throw.");
        }

        [Fact]
        public void DoesNotMatch_Match_ShouldThrow()
        {
            try
            {
                Assert("foo").DoesNotMatch(new Regex("foo"));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("DoesNotMatch failed. String 'foo' matches pattern 'foo'.");
                return;
            }

            Fail("DoesNotMatch did not throw.");
        }

        [Fact]
        public void DoesNotMatch_Mismatch_ShouldNotThrow() => Assert("foo").DoesNotMatch(new Regex("bar"));

        [Fact]
        public void EndsWith_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").EndsWith("bar", "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. EndsWith failed. String 'foo' does not end with string 'bar'.");
                return;
            }

            Fail("EndsWith did not throw.");
        }

        [Fact]
        public void EndsWith_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").EndsWith("bar", "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. EndsWith failed. String 'foo' does not end with string 'bar'.");
                return;
            }

            Fail("EndsWith did not throw.");
        }

        [Fact]
        public void EndsWith_NonSuffix_ShouldThrow()
        {
            try
            {
                Assert("foo").EndsWith("bar");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EndsWith failed. String 'foo' does not end with string 'bar'.");
                return;
            }

            Fail("EndsWith did not throw.");
        }

        [Fact]
        public void EndsWith_Suffix_ShouldNotThrow() => Assert("foo").EndsWith("o");

        [Fact]
        public void Matches_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").Matches(new Regex("bar"), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Matches failed. String 'foo' does not match pattern 'bar'.");
                return;
            }

            Fail("Matches did not throw.");
        }

        [Fact]
        public void Matches_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").Matches(new Regex("bar"), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Matches failed. String 'foo' does not match pattern 'bar'.");
                return;
            }

            Fail("Matches did not throw.");
        }

        [Fact]
        public void Matches_Match_ShouldNotThrow() => Assert("foo").Matches(new Regex("foo"));

        [Fact]
        public void Matches_Mismatch_ShouldThrow()
        {
            try
            {
                Assert("foo").Matches(new Regex("bar"));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Matches failed. String 'foo' does not match pattern 'bar'.");
                return;
            }

            Fail("Matches did not throw.");
        }

        [Fact]
        public void StartsWith_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").StartsWith("bar", "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. StartsWith failed. String 'foo' does not start with string 'bar'.");
                return;
            }

            Fail("StartsWith did not throw.");
        }

        [Fact]
        public void StartsWith_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").StartsWith("bar", "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. StartsWith failed. String 'foo' does not start with string 'bar'.");
                return;
            }

            Fail("StartsWith did not throw.");
        }

        [Fact]
        public void StartsWith_NonPrefix_ShouldThrow()
        {
            try
            {
                Assert("foo").StartsWith("bar");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("StartsWith failed. String 'foo' does not start with string 'bar'.");
                return;
            }

            Fail("StartsWith did not throw.");
        }

        [Fact]
        public void StartsWith_Prefix_ShouldNotThrow() => Assert("foo").StartsWith("f");
    }
}