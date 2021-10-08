namespace Testify.Tests;

[Unit]
public class StandardAssertionsTests
{
    private readonly AnonymousData anon = new();

    [Fact]
    public void IsEqualTo_SameValues_Succeeds()
    {
        var value = anon.AnyInt32();
        Assert(() => Assert(value).IsEqualTo(value)).DoesNotThrow();
    }

    [Fact]
    public void IsEqualTo_DifferentValues_Fails()
    {
        var actual = anon.AnyInt32(10, 20);
        var expected = anon.AnyInt32(30, 40);

        Assert(() => Assert(actual).IsEqualTo(expected))
            .Throws<XunitException>(e => Verify(e.Message).IsEqualTo($"Expected \"actual\" to be {expected}, but found {actual}."));
    }

    [Fact]
    public void IsEqualTo_DifferentValuesAndBecauseText_Fails()
    {
        var actual = anon.AnyInt32(10, 20);
        var expected = anon.AnyInt32(30, 40);

        Assert(() => Assert(actual).IsEqualTo(expected, because: "I said so"))
            .Throws<XunitException>(e => Verify(e.Message).IsEqualTo($"Expected \"actual\" to be {expected} because I said so, but found {actual}."));
    }

    [Fact]
    public void IsEqualTo_EqualAccordingToComparer_Succeeds()
    {
        var actual = anon.AnyString().ToLower();
        var expected = actual.ToUpper();

        Assert(() => Assert(actual).IsEqualTo(expected, comparer: StringComparer.OrdinalIgnoreCase))
            .DoesNotThrow();
    }
}
