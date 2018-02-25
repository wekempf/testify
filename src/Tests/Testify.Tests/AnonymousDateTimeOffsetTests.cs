using System;
using Xunit;

namespace Testify
{
    public class AnonymousDateTimeOffsetTests
    {
        [Fact]
        public void AnyDateTimeOffset()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTimeOffset>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTimeOffset.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTimeOffset.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTimeOffset());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfDateTimeOffset()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTimeOffset>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTimeOffset.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTimeOffset.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.Any<DateTimeOffset>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTimeOffset_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTimeOffset>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTimeOffset.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTimeOffset.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTimeOffset(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTimeOffset_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTimeOffset>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTimeOffset.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTimeOffset.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDateTimeOffset_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<DateTimeOffset>();
            classifier.AddClassification("GT", d => d.Ticks > (DateTimeOffset.MaxValue.Ticks / 2));
            classifier.AddClassification("LT", d => d.Ticks < (DateTimeOffset.MaxValue.Ticks / 2));

            classifier.Classify(() => anon.AnyDateTimeOffset(
                DateTimeOffset.MinValue,
                DateTimeOffset.MaxValue,
                Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}