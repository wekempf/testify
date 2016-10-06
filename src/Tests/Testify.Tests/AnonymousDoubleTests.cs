using Xunit;

namespace Testify
{
    public class AnonymousDoubleTests
    {
        [Fact]
        public void AnyDouble()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDouble());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDouble_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDouble(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyPositiveDouble()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveDouble();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveDoubleDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveDouble(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyNegativeDouble()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeDouble();

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnyNegativeDoubleDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeDouble(Distribution.Uniform);

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnyDouble_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDouble(double.MinValue, double.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}