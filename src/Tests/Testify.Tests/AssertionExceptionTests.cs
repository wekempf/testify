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
            var inner = new ArgumentNullException();

            var result = new AssertionException(inner);

            Assert.Equal("An assertion failed. (Value cannot be null.)", result.Message);
            Assert.Equal(inner, result.InnerException);
            Assert.Equal(new[] { inner }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_InnerExceptions_ShouldContainInnerExceptions()
        {
            var inner1 = new ArgumentNullException();
            var inner2 = new ArgumentNullException();

            var result = new AssertionException(inner1, inner2);

            Assert.Equal("An assertion failed. (Value cannot be null.) (Value cannot be null.)", result.Message);
            Assert.Equal(inner1, result.InnerException);
            Assert.Equal(new[] { inner1, inner2 }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_InnerExceptionsList_ShouldContainInnerExceptions()
        {
            var inner = new[] { new ArgumentNullException(), new ArgumentNullException() };

            var result = new AssertionException(inner.ToList());

            Assert.Equal("An assertion failed. (Value cannot be null.) (Value cannot be null.)", result.Message);
            Assert.Equal(inner[0], result.InnerException);
            Assert.Equal(inner, result.InnerExceptions);
        }

        [Fact]
        public void Construct_MessageAndInnerException_ShouldContainMessageAndInnerException()
        {
            var inner = new ArgumentNullException();

            var result = new AssertionException("Some message.", inner);

            Assert.Equal("Some message. (Value cannot be null.)", result.Message);
            Assert.Equal(inner, result.InnerException);
            Assert.Equal(new[] { inner }, result.InnerExceptions);
        }

        [Fact]
        public void Construct_MessageAndInnerExceptions_ShouldContainMessageAndInnerExceptions()
        {
            var inner = new[] { new ArgumentNullException(), new ArgumentNullException() };

            var result = new AssertionException("Some message.", inner);

            Assert.Equal("Some message. (Value cannot be null.) (Value cannot be null.)", result.Message);
            Assert.Equal(inner[0], result.InnerException);
            Assert.Equal(inner, result.InnerExceptions);
        }

        [Fact]
        public void Construct_MessageAndInnerExceptionsList_ShouldContainMessageAndInnerExceptions()
        {
            var inner = new[] { new ArgumentNullException(), new ArgumentNullException() };

            var result = new AssertionException("Some message.", inner.ToList());

            Assert.Equal("Some message. (Value cannot be null.) (Value cannot be null.)", result.Message);
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