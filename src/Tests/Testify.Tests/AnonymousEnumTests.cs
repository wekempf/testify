using System;
using Xunit;

namespace Testify
{
    public class EnumFactoryTests
    {
        [Flags]
        private enum FlagsEnum
        {
            None = 0x0,
            First = 0x1,
            Second = 0x2,
            Third = 0x4
        }

        private enum SimpleEnum
        {
            One,
            Two,
            Three
        }

        [Fact]
        public void AnyEnumValueOfNonEnumDistribution()
        {
            var anon = new AnonymousData();

            try
            {
                anon.AnyEnumValue<string>(Distribution.Uniform);
            }
            catch (InvalidOperationException)
            {
                return;
            }

            Assertions.Fail("AnyEnumValue did not throw.");
        }

        [Fact]
        public void AnyEnumValueOfNonEnum()
        {
            var anon = new AnonymousData();

            try
            {
                anon.AnyEnumValue<string>();
            }
            catch (InvalidOperationException)
            {
                return;
            }

            Assertions.Fail("AnyEnumValue did not throw.");
        }

        [Fact]
        public void AnyEnumValue_FlagsEnumType()
        {
            var anon = new AnonymousData();

            var result = (FlagsEnum)anon.AnyEnumValue(typeof(FlagsEnum));
        }

        [Fact]
        public void AnyEnumValue_FlagsEnumTypeDistribution()
        {
            var anon = new AnonymousData();

            var result = (FlagsEnum)anon.AnyEnumValue(typeof(FlagsEnum), Distribution.Uniform);
        }

        [Fact]
        public void AnyEnumValue_SimpleEnumType()
        {
            var anon = new AnonymousData();

            var result = (SimpleEnum)anon.AnyEnumValue(typeof(SimpleEnum));
        }

        [Fact]
        public void AnyEnumValue_SimpleEnumTypeDistribution()
        {
            var anon = new AnonymousData();

            var result = (SimpleEnum)anon.AnyEnumValue(typeof(SimpleEnum), Distribution.Uniform);
        }

        [Fact]
        public void AnyEnumValueOfFlagsEnum()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumValue<FlagsEnum>();
        }

        [Fact]
        public void AnyEnumValueOfFlagsEnum_Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumValue<FlagsEnum>(Distribution.Uniform);
        }

        [Fact]
        public void AnyEnumValueOfSimpleEnum()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumValue<SimpleEnum>();
        }

        [Fact]
        public void AnyEnumValueOfSimpleEnum_Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumValue<SimpleEnum>(Distribution.Uniform);
        }
    }
}