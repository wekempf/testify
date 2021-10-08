namespace Testify.Tests;

[Unit]
public class AnonymousEnumerableTests
{
    [Fact]
    public void AnyEnumerable_WithDefaults_ReturnsEnumerable()
    {
        var anon = new AnonymousData();

        var result = anon.AnyEnumerable<double>();
        var count = result.Count();

        // Not using AssertAll because we can't make more assertions if result is null
        Assert(result).IsNotNull();
        Assert(count).IsInRange(2, 10);
    }

    [Fact]
    public void Any_WithIEnumerableType_ReturnsEnumerable()
    {
        var anon = new AnonymousData();

        var result = anon.Any<IEnumerable<double>>();
        var count = result.Count();

        // Not using AssertAll because we can't make more assertions if result is null
        Assert(result).IsNotNull();
        Assert(count).IsInRange(2, 10);
    }
}
