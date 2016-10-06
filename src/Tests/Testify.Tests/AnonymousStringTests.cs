using System.Linq;
using Xunit;

namespace Testify
{
    public class AnonymousStringTests
    {
        [Fact]
        public void AnyString()
        {
            var anon = new AnonymousData();

            var result = anon.AnyString();

            Assert.NotNull(result);
            Assert.True(result.All(c => char.IsLetter(c)));
        }

        [Fact]
        public void AnyString_Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyString(Distribution.Uniform);

            Assert.NotNull(result);
            Assert.True(result.All(c => char.IsLetter(c)));
        }

        [Fact]
        public void AnyString_MinMaxLength()
        {
            var anon = new AnonymousData();

            var result = anon.AnyString(4, 10);

            Assert.NotNull(result);
            Assert.True(result.All(c => char.IsLetter(c)));
            Assert.True(result.Length >= 4 && result.Length <= 10);
        }

        [Fact]
        public void AnyString_MinMaxLengthDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyString(4, 10, Distribution.Uniform);

            Assert.NotNull(result);
            Assert.True(result.All(c => char.IsLetter(c)));
            Assert.True(result.Length >= 4 && result.Length <= 10);
        }

        [Fact]
        public void AnyFirstName()
        {
            var anon = new AnonymousData();

            var name = anon.AnyFirstName();

            Assert.NotNull(name);
        }

        [Fact]
        public void AnySurname()
        {
            var anon = new AnonymousData();

            var name = anon.AnySurname();

            Assert.NotNull(name);
        }

        [Fact]
        public void AnyFullName()
        {
            var anon = new AnonymousData();

            var name = anon.AnyFullName();

            Assert.NotNull(name);
        }
    }
}