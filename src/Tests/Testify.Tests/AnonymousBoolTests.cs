using Xunit;

namespace Testify
{
    public class AnonymousBoolTests
    {
        [Fact]
        public void TestAnyBool()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<bool>();
            classifier.AddClassification("true", b => b);
            classifier.AddClassification("false", b => !b);

            classifier.Classify(() => anon.AnyBool());

            Assert.True(classifier["true"] > 0.4);
            Assert.True(classifier["false"] > 0.4);
        }

        [Fact]
        public void TestAnyOfBool()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<bool>();
            classifier.AddClassification("true", b => b);
            classifier.AddClassification("false", b => !b);

            classifier.Classify(() => anon.Any<bool>());

            Assert.True(classifier["true"] > 0.4);
            Assert.True(classifier["false"] > 0.4);
        }

        [Fact]
        public void TestAnyBool_Distribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<bool>();
            classifier.AddClassification("true", b => b);
            classifier.AddClassification("false", b => !b);

            classifier.Classify(() => anon.AnyBool(Distribution.Uniform));

            Assert.True(classifier["true"] > 0.4);
            Assert.True(classifier["false"] > 0.4);
        }
    }
}