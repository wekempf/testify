namespace Testify;

public class AnonymousBoolTests
{
    private readonly AnonymousData anon = new();
    private readonly Classifier<bool> classifier;

    public AnonymousBoolTests()
    {
        classifier = new();
        classifier.AddClassification("true", b => b);
        classifier.AddClassification("false", b => !b);
    }

    [Fact]
    public void TestAnyBool()
    {
        classifier.Classify(() => anon.AnyBool());

        AssertAll(
            Verify(classifier["true"]).IsGreaterThan(0.4),
            Verify(classifier["false"]).IsGreaterThan(0.4));
    }

    [Fact]
    public void TestAnyOfBool()
    {
        classifier.Classify(() => anon.Any<bool>());

        AssertAll(
            Verify(classifier["true"]).IsGreaterThan(0.4),
            Verify(classifier["false"]).IsGreaterThan(0.4));
    }

    [Fact]
    public void TestAnyBool_Distribution()
    {
        classifier.Classify(() => anon.AnyBool(Distribution.Uniform));

        AssertAll(
            Verify(classifier["true"]).IsGreaterThan(0.4),
            Verify(classifier["false"]).IsGreaterThan(0.4));
    }
}