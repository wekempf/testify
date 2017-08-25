using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Testify.Tests
{
    public class AnonymousDataExceptionTests
    {
        [Fact]
        public void Ctor_Type()
        {
            var type = typeof(string);

            var exception = new AnonymousDataException(type);

            Assert.Equal($"Unable to create instance of specified type {typeof(string)}.", exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(type, exception.AnonymousType);
        }

        [Fact]
        public void Ctor_TypeInner()
        {
            var type = typeof(string);
            var inner = new Exception();

            var exception = new AnonymousDataException(type, inner);

            Assert.Equal($"Unable to create instance of specified type {typeof(string)}.", exception.Message);
            Assert.Same(inner, exception.InnerException);
            Assert.Equal(type, exception.AnonymousType);
        }

        [Fact]
        public void Ctor_Property()
        {
            var type = typeof(List<int>);
            var property = type.GetProperty(nameof(List<int>.Count));

            var exception = new AnonymousDataException(property);

            Assert.Equal($"Unable to populate property {nameof(List<int>.Count)} on type {typeof(List<int>)}.", exception.Message);
            Assert.Null(exception.InnerException);
            Assert.Equal(type, exception.AnonymousType);
        }

        [Fact]
        public void Ctor_PropertyInner()
        {
            var type = typeof(List<int>);
            var property = type.GetProperty(nameof(List<int>.Count));
            var inner = new Exception();

            var exception = new AnonymousDataException(property, inner);

            Assert.Equal($"Unable to populate property {nameof(List<int>.Count)} on type {typeof(List<int>)}.", exception.Message);
            Assert.Same(inner, exception.InnerException);
            Assert.Equal(type, exception.AnonymousType);
        }
    }
}