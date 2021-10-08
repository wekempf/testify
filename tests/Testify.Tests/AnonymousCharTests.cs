namespace Testify;

public class AnonymousCharTests
{
    private readonly AnonymousData anon = new();
    private readonly Classifier<char> classifier = new();

    public AnonymousCharTests()
    {
        classifier = new();
    }

    [Fact]
    public void AnyAlphaChar()
    {
        classifier.AddClassification("alpha", d => char.IsLetter(d));

        classifier.Classify(() => anon.AnyAlphaChar());

        Assert(classifier["alpha"]).IsEqualTo(1.0, because: "all generated characters should be an alpha character");
    }

    [Fact]
    public void AnyAlphaChar_Distribution()
    {
        classifier.AddClassification("lower", d => char.ToLower(d) == d);
        classifier.AddClassification("upper", d => char.ToUpper(d) == d);

        classifier.Classify(() => anon.AnyAlphaChar(Distribution.Uniform));

        AssertAll(
            Verify(classifier["lower"]).IsGreaterThan(0.4, because: "roughly have the characters should be lowercase"),
            Verify(classifier["upper"]).IsGreaterThan(0.4, because: "roughly have the characters should be uppercase"));
    }

    [Fact]
    public void AnyAlphaNumericChar()
    {
        classifier.AddClassification("alphaNumeric", d => char.IsLetterOrDigit(d));

        classifier.Classify(() => anon.AnyAlphaNumericChar());

        Assert(classifier["alphaNumeric"]).IsEqualTo(1.0, because: "all generated characters should be alpha numeric");
    }

    [Fact]
    public void AnyAlphaNumericChar_Distribution()
    {
        classifier.AddClassification("alphaNumeric", d => char.IsLetterOrDigit(d));
        classifier.AddClassification("lower", d => char.IsLetter(d) && char.IsLower(d));
        classifier.AddClassification("upper", d => char.IsLetter(d) && char.IsUpper(d));
        classifier.AddClassification("numeric", d => char.IsNumber(d));

        classifier.Classify(() => anon.AnyAlphaNumericChar(Distribution.Uniform));

        AssertAll(
            Verify(classifier["alphaNumeric"]).IsEqualTo(1.0, because: "all generated characters should be alpha numeric characters"),
            Verify(classifier["lower"]).IsGreaterThan(0.42, because: "roughly 42% of the generated characters should be a lower case letter"),
            Verify(classifier["upper"]).IsGreaterThan(0.42, because: "roughly 42% of the generated characters should be an upper case letter"),
            Verify(classifier["numeric"]).IsGreaterThan(0.12, because: "roughly 12% of the generated characters should be numeric"));
    }

    [Fact]
    public void AnyBasicLatinChar()
    {
        classifier.AddClassification("latin", d => d >= 0x20 && d <= 0x7f);

        classifier.Classify(() => anon.AnyBasicLatinChar());

        Assert(classifier["latin"]).IsEqualTo(1.0, because: "all generated characters should be latin characters");
    }

    [Fact]
    public void AnyBasicLatinChar_Distribution()
    {
        classifier.AddClassification("latin", d => d >= 0x20 && d <= 0x7f);
        classifier.AddClassification("lower", d => d < 0x4f);
        classifier.AddClassification("upper", d => d > 0x4f);

        classifier.Classify(() => anon.AnyBasicLatinChar(Distribution.Uniform));

        AssertAll(
            Verify(classifier["latin"]).IsEqualTo(1.0, because: "all generated characters should be latin characters"),
            Verify(classifier["lower"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the lower half of latin characters"),
            Verify(classifier["upper"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the upper half of latin characters"));
    }

    [Fact]
    public void AnyChar()
    {
        classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
        classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

        classifier.Classify(() => anon.AnyChar());

        AssertAll(
            Verify(classifier["GT"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the upper half"),
            Verify(classifier["LT"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the lower half"));
    }

    [Fact]
    public void AnyOfChar()
    {
        classifier.AddClassification("alphaNumeric", d => char.IsLetterOrDigit(d));
        classifier.AddClassification("lower", d => char.IsLetter(d) && char.IsLower(d));
        classifier.AddClassification("upper", d => char.IsLetter(d) && char.IsUpper(d));
        classifier.AddClassification("numeric", d => char.IsNumber(d));

        classifier.Classify(() => anon.Any<char>());

        AssertAll(
            Verify(classifier["alphaNumeric"]).IsEqualTo(1.0, because: "all generated characters should be alpha numeric characters"),
            Verify(classifier["lower"]).IsGreaterThan(0.42, because: "roughly 42% of the generated characters should be a lower case letter"),
            Verify(classifier["upper"]).IsGreaterThan(0.42, because: "roughly 42% of the generated characters should be an upper case letter"),
            Verify(classifier["numeric"]).IsGreaterThan(0.12, because: "roughly 12% of the generated characters should be numeric"));
    }

    [Fact]
    public void AnyChar_MinMaxDistribution()
    {
        classifier.AddClassification("GT", d => d > (char.MaxValue / 2));
        classifier.AddClassification("LT", d => d < (char.MaxValue / 2));

        classifier.Classify(() => anon.AnyChar(char.MinValue, char.MaxValue, Distribution.Uniform));

        AssertAll(
            Verify(classifier["GT"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the upper half"),
            Verify(classifier["LT"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the lower half"));
    }

    [Fact]
    public void AnyLatinSupplementChar()
    {
        classifier.AddClassification("latinSupplement", d => d >= 0xa0 && d <= 0xff);

        classifier.Classify(() => anon.AnyLatinSupplementChar());

        Assert(classifier["latinSupplement"]).IsEqualTo(1.0, because: "all generated characters should be latin supplement characters");
    }

    [Fact]
    public void AnyLatinSupplementChar_Distribution()
    {
        classifier.AddClassification("latinSupplement", d => d >= 0xa0 && d <= 0xff);
        classifier.AddClassification("lower", d => d < 0xcf);
        classifier.AddClassification("upper", d => d > 0xcf);

        classifier.Classify(() => anon.AnyLatinSupplementChar(Distribution.Uniform));

        AssertAll(
            Verify(classifier["latinSupplement"]).IsEqualTo(1.0, because: "all generated characters should be latin supplement characters"),
            Verify(classifier["lower"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the lower half of latin supplement characters"),
            Verify(classifier["upper"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the upper half of latin supplement characters"));
    }

    [Fact]
    public void AnyNumericChar()
    {
        classifier.AddClassification("numeric", d => char.IsDigit(d));

        classifier.Classify(() => anon.AnyNumericChar());

        Assert(classifier["numeric"]).IsEqualTo(1.0, because: "all generated characters should be numeric characters");
    }

    [Fact]
    public void AnyNumericChar_Distribution()
    {
        classifier.AddClassification("numeric", d => char.IsDigit(d));
        classifier.AddClassification("lower", d => d < '4'); // Range 0-9
        classifier.AddClassification("upper", d => d > '4'); // Range 0-9

        classifier.Classify(() => anon.AnyNumericChar(Distribution.Uniform));

        AssertAll(
            Verify(classifier["numeric"]).IsEqualTo(1.0, because: "all generated characters should be numeric characters"),
            Verify(classifier["lower"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the lower half of numeric characters"),
            Verify(classifier["upper"]).IsGreaterThan(0.4, because: "roughly half the generated characters should be in the upper half of numeric characters"));
    }

    [Fact]
    public void AnyPrintableChar()
    {
        classifier.AddClassification("printable", d => (d >= 0x20 && d <= 0x7f) || (d >= 0xa0 && d <= 0xff));

        classifier.Classify(() => anon.AnyPrintableChar());

        Assert(classifier["printable"]).IsEqualTo(1.0, because: "all generated characters should be printable characters");
    }

    [Fact]
    public void AnyPrintableChar_Distribution()
    {
        classifier.AddClassification("printable", d => (d >= 0x20 && d <= 0x7f) || (d >= 0xa0 && d <= 0xff));

        classifier.Classify(() => anon.AnyPrintableChar(Distribution.Uniform));

        Assert(classifier["printable"]).IsEqualTo(1.0, because: "all generated characters should be printable characters");
    }
}
