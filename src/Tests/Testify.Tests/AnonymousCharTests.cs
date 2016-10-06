using Xunit;

namespace Testify
{
    public class AnonymousCharTests
    {
        [Fact]
        public void AnyAlphaChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsLetter(d));

            classifier.Classify(() => anon.AnyAlphaChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyAlphaChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsLetter(d));

            classifier.Classify(() => anon.AnyAlphaChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyAlphaNumericChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsLetterOrDigit(d));

            classifier.Classify(() => anon.AnyAlphaNumericChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyAlphaNumericChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsLetterOrDigit(d));

            classifier.Classify(() => anon.AnyAlphaNumericChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyBasicLatinChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => d >= 0x20 && d <= 0x7f);

            classifier.Classify(() => anon.AnyBasicLatinChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyBasicLatinChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => d >= 0x20 && d <= 0x7f);

            classifier.Classify(() => anon.AnyBasicLatinChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

            classifier.Classify(() => anon.AnyChar());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

            classifier.Classify(() => anon.Any<char>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

            classifier.Classify(() => anon.AnyChar(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyChar_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

            classifier.Classify(() => anon.AnyChar(char.MinValue, char.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyChar_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

            classifier.Classify(() => anon.AnyChar(char.MinValue, char.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyLatinSupplementChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => d >= 0xa0 && d <= 0xff);

            classifier.Classify(() => anon.AnyLatinSupplementChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyLatinSupplementChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => d >= 0xa0 && d <= 0xff);

            classifier.Classify(() => anon.AnyLatinSupplementChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyNumericChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsDigit(d));

            classifier.Classify(() => anon.AnyNumericChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyNumericChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => char.IsDigit(d));

            classifier.Classify(() => anon.AnyNumericChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyPrintableChar()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => (d >= 0x20 && d <= 0x7f) || (d >= 0xa0 && d <= 0xff));

            classifier.Classify(() => anon.AnyPrintableChar());

            Assert.True(classifier["Alpha"] == 1.0);
        }

        [Fact]
        public void AnyPrintableChar_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<char>();
            classifier.AddClassification("Alpha", d => (d >= 0x20 && d <= 0x7f) || (d >= 0xa0 && d <= 0xff));

            classifier.Classify(() => anon.AnyPrintableChar(Distribution.Uniform));

            Assert.True(classifier["Alpha"] == 1.0);
        }
    }
}