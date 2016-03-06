using Xunit;

namespace Testify
{
    public class Int32FactoryTests
    {
        [Fact]
        public void AnyInt32()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<int>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt32());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyNegativeInt32()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt32();

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyNegativeInt32Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeInt32(Distribution.Uniform);

            Assert.True(result < 0);
        }

        [Fact]
        public void AnyPositiveInt32()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt32();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveInt32Distribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveInt32(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyInt32_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<int>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt32(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt32_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<int>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt32(int.MinValue, int.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyInt32_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<int>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyInt32(int.MinValue, int.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}