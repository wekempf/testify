using System;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class AnonymousTimeSpanTests
    {
        [Fact]
        public void AnyTimeSpan()
        {
            var anon = new AnonymousData();
            var mid = new TimeSpan(0);
            var classifier = new Classifier<TimeSpan>();
            classifier.AddClassification("GT", d => d > mid);
            classifier.AddClassification("LT", d => d < mid);

            classifier.Classify(() => anon.AnyTimeSpan());

            AssertAll(
                "Distribution of values was not uniform.",
                () => Assert(classifier["GT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be greater than {mid}. Actual: {classifier["GT"]}."),
                () => Assert(classifier["LT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be less than {mid}. Actual: {classifier["LT"]}."));
        }

        [Fact]
        public void AnyOfTimeSpan()
        {
            var anon = new AnonymousData();
            var mid = new TimeSpan(0);
            var classifier = new Classifier<TimeSpan>();
            classifier.AddClassification("GT", d => d > mid);
            classifier.AddClassification("LT", d => d < mid);

            classifier.Classify(() => anon.Any<TimeSpan>());

            AssertAll(
                "Distribution of values was not uniform.",
                () => Assert(classifier["GT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be greater than {mid}. Actual: {classifier["GT"]}."),
                () => Assert(classifier["LT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be less than {mid}. Actual: {classifier["LT"]}."));
        }

        [Fact]
        public void AnyTimeSpan_Distribution()
        {
            var anon = new AnonymousData();
            var mid = new TimeSpan(0);
            var classifier = new Classifier<TimeSpan>();
            classifier.AddClassification("GT", d => d > mid);
            classifier.AddClassification("LT", d => d < mid);

            classifier.Classify(() => anon.AnyTimeSpan(Distribution.Uniform));

            AssertAll(
                "Distribution of values was not uniform.",
                () => Assert(classifier["GT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be greater than {mid}. Actual: {classifier["GT"]}."),
                () => Assert(classifier["LT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be less than {mid}. Actual: {classifier["LT"]}."));
        }

        [Fact]
        public void AnyTimeSpan_MinMax()
        {
            var anon = new AnonymousData();
            var mid = new TimeSpan(0);
            var classifier = new Classifier<TimeSpan>();
            classifier.AddClassification("GT", d => d > mid);
            classifier.AddClassification("LT", d => d < mid);

            classifier.Classify(() => anon.AnyTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue));

            AssertAll(
                "Distribution of values was not uniform.",
                () => Assert(classifier["GT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be greater than {mid}. Actual: {classifier["GT"]}."),
                () => Assert(classifier["LT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be less than {mid}. Actual: {classifier["LT"]}."));
        }

        [Fact]
        public void AnyTimeSpan_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var mid = new TimeSpan(0);
            var classifier = new Classifier<TimeSpan>();
            classifier.AddClassification("GT", d => d > mid);
            classifier.AddClassification("LT", d => d < mid);

            classifier.Classify(() => anon.AnyTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue, Distribution.Uniform));

            AssertAll(
                "Distribution of values was not uniform.",
                () => Assert(classifier["GT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be greater than {mid}. Actual: {classifier["GT"]}."),
                () => Assert(classifier["LT"] > 0.4)
                    .IsTrue($"Roughly half the generated values should be less than {mid}. Actual: {classifier["LT"]}."));
        }
    }
}