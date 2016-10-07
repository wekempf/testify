using Xunit;

namespace Testify.Tests
{
    public class AnonymousDecimalTests
    {
        [Fact]
        public void AnyDecimal()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<decimal>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDecimal());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDecimal_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<decimal>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDecimal(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyPositiveDecimal()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveDecimal();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveDecimalDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveDecimal(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyNegativeDecimal()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeDecimal();

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnyNegativeDecimalDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeDecimal(Distribution.Uniform);

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnyDecimal_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<decimal>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDecimal(decimal.MinValue, decimal.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}