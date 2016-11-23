using System;
using Xunit;

namespace Testify
{
    public class AnonymousDateTimeTests
    {
        [Fact]
        public void AnyDateTime()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTime>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTime.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTime.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTime());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfDateTime()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTime>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTime.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTime.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.Any<DateTime>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTime_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTime>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTime.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTime.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTime(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTime_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTime>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTime.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTime.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTime(DateTime.MinValue, DateTime.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTime_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTime>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTime.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTime.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTime(DateTime.MinValue, DateTime.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}