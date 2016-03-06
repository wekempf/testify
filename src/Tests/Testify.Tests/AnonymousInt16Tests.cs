using Xunit;

namespace Testify
{
    public class Int16FactoryTests
    {
        [Fact]
        public void AnyInt16()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<short>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt16());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfInt16()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<short>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.Any<short>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyNegativeInt16()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt16();

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyNegativeInt16Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt16(Distribution.Uniform);

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyPositiveInt16()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt16();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveInt16Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt16(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyInt16_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<short>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt16(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt16_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<short>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt16(short.MinValue, short.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt16_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<short>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt16(short.MinValue, short.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}