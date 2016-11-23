using Xunit;

namespace Testify
{
    public class AnonymousInt64Tests
    {
        [Fact]
        public void AnyInt64()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<long>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt64());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfInt64()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<long>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.Any<long>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyNegativeInt64()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt64();

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyNegativeInt64Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt64(Distribution.Uniform);

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyPositiveInt64()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt64();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveInt64Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt64(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyInt64_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<long>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt64(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt64_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<long>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt64(long.MinValue, long.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt64_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<long>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt64(long.MinValue, long.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}