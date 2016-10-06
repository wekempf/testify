using Xunit;

namespace Testify
{
    public class AnonymousSingleTests
    {
        [Fact]
        public void AnySingle()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<float>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnySingle());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfSingle()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<float>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.Any<float>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyPositiveSingle()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveSingle();

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyPositiveSingleDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyPositiveSingle(Distribution.Uniform);

            Assert.True(result >= 0);
        }

        [Fact]
        public void AnyNegativeSingle()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeSingle();

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnyNegativeSingleDistribution()
        {
            var anon = new AnonymousData();

            var result = anon.AnyNegativeSingle(Distribution.Uniform);

            Assert.True(result <= 0);
        }

        [Fact]
        public void AnySingle_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<float>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnySingle(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnySingle_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<float>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnySingle(float.MinValue, float.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnySingle_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<float>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnySingle(float.MinValue, float.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}