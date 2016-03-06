using Xunit;

namespace Testify
{
    public class ByteFactoryTests
    {
        [Fact]
        public void AnyByte()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<byte>();
            classifier.AddClassification("GT", d => d > (byte.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (byte.MaxValue / 2));

            classifier.Classify(() => anon.AnyByte());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyOfByte()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<byte>();
            classifier.AddClassification("GT", d => d > (byte.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (byte.MaxValue / 2));

            classifier.Classify(() => anon.Any<byte>());

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyByte_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<byte>();
            classifier.AddClassification("GT", d => d > (byte.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (byte.MaxValue / 2));

            classifier.Classify(() => anon.AnyByte(Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyByte_MinMax()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<byte>();
            classifier.AddClassification("GT", d => d > (byte.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (byte.MaxValue / 2));

            classifier.Classify(() => anon.AnyByte(byte.MinValue, byte.MaxValue));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyByte_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<byte>();
            classifier.AddClassification("GT", d => d > (byte.MaxValue / 2));
            classifier.AddClassification("LT", d => d < (byte.MaxValue / 2));

            classifier.Classify(() => anon.AnyByte(byte.MinValue, byte.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }
    }
}