using System;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class ExceptionAssertionsTests
    {
        [Fact]
        public void Throws_AdditionalFailingAssertion_ShouldThrow()
        {
            try
            {
                Assert(ThrowExceptionWithMessageFoo)
                    .Throws<Exception>(e => Assert(e.Message).IsEqualTo("bar"));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Throws failed. Caught the expected exception type <{typeof(Exception)}>, but additional assertions failed.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_AdditionalPassingAssertion_ShouldNotThrow()
        {
            Assert(ThrowExceptionWithMessageFoo)
                .Throws<Exception>(e => Assert(e.Message).IsEqualTo("foo"));
        }

        [Fact]
        public void Throws_ExpectedBaseException_ShouldNotThrow()
        {
            Assert(Throw<ArgumentNullException>).Throws<ArgumentException>();
        }

        [Fact]
        public void Throws_ExpectedException_ShouldNotThrow()
        {
            Assert(Throw<Exception>).Throws<Exception>();
        }

        [Fact]
        public void Throws_MessageAdditionalFailingAssertion_ShouldDisplayMessage()
        {
            try
            {
                Assert(ThrowExceptionWithMessageFoo)
                    .Throws<Exception>(e => Assert(e.Message).IsEqualTo("bar"), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Caught the expected exception type <{typeof(Exception)}>, but additional assertions failed.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_MessageAdditionalPassingAssertion_ShouldNotThrow()
        {
            Assert(ThrowExceptionWithMessageFoo)
                .Throws<Exception>(e => Assert(e.Message).IsEqualTo("foo"), "Some message.");
        }

        [Fact]
        public void Throws_MessageExpectedBaseException_ShouldNotThrow()
        {
            Assert(Throw<ArgumentNullException>).Throws<ArgumentException>("Some message.");
        }

        [Fact]
        public void Throws_MessageExpectedException_ShouldNotThrow()
        {
            Assert(Throw<Exception>).Throws<Exception>("Some message.");
        }

        [Fact]
        public void Throws_MessageNoThrow_ShouldDisplayMessage()
        {
            try
            {
                Assert(NoThrow).Throws<Exception>("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Expected exception: <{typeof(Exception)}>. The action did not throw any exceptions.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_MessageParametersAdditionalFailingAssertion_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(ThrowExceptionWithMessageFoo)
                    .Throws<Exception>(e => Assert(e.Message).IsEqualTo("bar"), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Caught the expected exception type <{typeof(Exception)}>, but additional assertions failed.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_MessageParametersAdditionalPassingAssertion_ShouldNotThrow()
        {
            Assert(ThrowExceptionWithMessageFoo)
                .Throws<Exception>(e => Assert(e.Message).IsEqualTo("foo"), "Some {0}.", "message");
        }

        [Fact]
        public void Throws_MessageParametersExpectedBaseException_ShouldNotThrow()
        {
            Assert(Throw<ArgumentNullException>).Throws<ArgumentException>("Some {0}.", "message");
        }

        [Fact]
        public void Throws_MessageParametersExpectedException_ShouldNotThrow()
        {
            Assert(Throw<Exception>).Throws<Exception>("Some {0}.", "message");
        }

        [Fact]
        public void Throws_MessageParametersNoThrow_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(NoThrow).Throws<Exception>("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Expected exception: <{typeof(Exception)}>. The action did not throw any exceptions.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_MessageParametersUnexpectedException_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(Throw<ArgumentNullException>).Throws<ArgumentOutOfRangeException>("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Expected exception: <{typeof(ArgumentOutOfRangeException)}>. Actual exception: <{typeof(ArgumentNullException)}>.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_MessageUnexpectedException_ShouldDisplayMessage()
        {
            try
            {
                Assert(Throw<ArgumentNullException>).Throws<ArgumentOutOfRangeException>("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. Throws failed. Expected exception: <{typeof(ArgumentOutOfRangeException)}>. Actual exception: <{typeof(ArgumentNullException)}>.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_NoThrow_ShouldThrow()
        {
            try
            {
                Assert(NoThrow).Throws<Exception>();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Throws failed. Expected exception: <{typeof(Exception)}>. The action did not throw any exceptions.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        [Fact]
        public void Throws_UnexpectedException_ShouldThrow()
        {
            try
            {
                Assert(Throw<ArgumentNullException>).Throws<ArgumentOutOfRangeException>();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Throws failed. Expected exception: <{typeof(ArgumentOutOfRangeException)}>. Actual exception: <{typeof(ArgumentNullException)}>.");
                return;
            }

            Fail("Throws did not throw an exception.");
        }

        private void NoThrow()
        {
        }

        private void Throw<T>()
            where T : Exception, new()
        {
            throw new T();
        }

        private void ThrowExceptionWithMessageFoo()
        {
            throw new Exception("foo");
        }
    }
}