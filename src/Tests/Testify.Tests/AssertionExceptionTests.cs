using System;
using System.Linq;
using Xunit;

namespace Testify
{
    public class AssertionExceptionTests
    {
        [Fact]
        public void Construct_InnerException_ShouldContainInnerException()
        {
            var inner = new InvalidOperationException("Failure.");

            var result = new AssertionException(inner);

            Assert.Equal("An assertion failed. (Failure.)", result.Message);
            Assert.Equal(inner, result.InnerException);
            Assert.Equal(new[] { inner }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_InnerExceptions_ShouldContainInnerExceptions()
        {
            var inner1 = new InvalidOperationException("Failure.");
            var inner2 = new InvalidOperationException("Failure.");

            var result = new AssertionException(inner1, inner2);

            Assert.Equal("An assertion failed. (Failure.) (Failure.)", result.Message);
            Assert.Equal(inner1, result.InnerException);
            Assert.Equal(new[] { inner1, inner2 }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_MessageAndInnerException_ShouldContainMessageAndInnerException()
        {
            var inner = new InvalidOperationException("Failure.");

            var result = new AssertionException("Some message.", inner);

            Assert.Equal("Some message. (Failure.)", result.Message);
            Assert.Equal(inner, result.InnerException);
            Assert.Equal(new[] { inner }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_MessageAndInnerExceptions_ShouldContainMessageAndInnerExceptions()
        {
            var inner = new[] { new InvalidOperationException("Failure."), new InvalidOperationException("Failure.") };

            var result = new AssertionException("Some message.", inner);

            Assert.Equal("Some message. (Failure.) (Failure.)", result.Message);
            Assert.Equal(inner[0], result.InnerException);
            Assert.Equal(inner, result.InnerExceptions);
        }

        [Fact]
        public void Construction_NoArgs_ShouldUseExpectedMessage()
        {
            var result = new AssertionException();

            Assert.Equal("An assertion failed.", result.Message);
            Assert.Null(result.InnerException);
            Assert.Empty(result.InnerExceptions);
        }
    }
}