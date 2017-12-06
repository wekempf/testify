using System;
using Xunit;

namespace Testify
{
    public class DistributionTests
    {
        private const string GT085 = "> 0.85";
        private const string LT0 = "< 0";
        private const string LT015 = "< 0.15";
        private const int RUNS = 1000;

        [Fact]
        public void TestInvertedNormalDistribution()
        {
            var classifications = Classify(Distribution.InvertedNormal);
            Assert.True(classifications[LT0] == 0);
            Assert.True(classifications[LT015] > .27);
            Assert.True(classifications[GT085] > .27);
        }

        [Fact]
        public void TestNegativeNormalDistribution()
        {
            var classifications = Classify(Distribution.NegativeNormal);
            Assert.True(classifications[LT0] == 0);
            Assert.True(classifications[LT015] < .02);
            Assert.True(classifications[GT085] > .25);
        }

        [Fact]
        public void TestPositiveNormalDistribution()
        {
            var classifications = Classify(Distribution.PositiveNormal);
            Assert.True(classifications[LT0] == 0);
            Assert.True(classifications[LT015] > .25);
            Assert.True(classifications[GT085] < .02);
        }

        [Fact]
        public void TestUniformDistribution()
        {
            var classifications = Classify(Distribution.Uniform);
            Assert.True(classifications[LT0] == 0, "LT0 == 0");
            Assert.True(classifications[LT015] < .20, $"LT015 ({classifications[LT015]}) < .20");
            Assert.True(classifications[GT085] < .20, $"GT085  ({classifications[GT085]}) < .20");
        }

        private Classifier<double> Classify(Distribution distribution)
        {
            var classifier = new Classifier<double>();
            classifier.AddClassification(LT015, d => d < 0.15);
            classifier.AddClassification(GT085, d => d > 0.85);
            classifier.AddClassification(LT0, d => d < 0);

            var random = new Random(0xBFAC);
            for (var i = 0; i < RUNS; ++i)
            {
                classifier.Classify(distribution.NextDouble(random));
            }

            return classifier;
        }
    }
}