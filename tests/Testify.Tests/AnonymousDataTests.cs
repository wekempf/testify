namespace Testify.Tests;

[Unit]
public class AnonymousDataTests
{
    private readonly AnonymousData anon = new AnonymousData();

    [Fact]
    public void AnyDouble_RandomDouble()
    {
        var classifier = new Classifier<double>();
        classifier.AddClassification("GT", d => d > 0);
        classifier.AddClassification("LT", d => d < 0);

        classifier.Classify(() => (double)anon.AnyDouble());

        AssertAll(
            Verify(classifier["GT"]).IsGreaterThan(0.4),
            Verify(classifier["LT"]).IsGreaterThan(0.4));
    }

    [Fact]
    public void AnyGenericDouble_RandomDouble()
    {
        var classifier = new Classifier<double>();
        classifier.AddClassification("GT", d => d > 0);
        classifier.AddClassification("LT", d => d < 0);

        classifier.Classify(() => anon.Any<double>());

        AssertAll(
            Verify(classifier["GT"]).IsGreaterThan(0.4),
            Verify(classifier["LT"]).IsGreaterThan(0.4));
    }
}
