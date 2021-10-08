namespace Testify;

/// <summary>
/// Extension methods for anonymous <see cref="long"/> type.
/// </summary>
public static class AnonymousInt64
{
    /// <summary>
    /// Creates a random <see langword="long"/> value within the specified range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="long"/> value.</returns>
    public static long AnyInt64(this IAnonymousData anon, long minimum = long.MinValue, long maximum = long.MaxValue, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);
        Guard.Against.OutOfRange(maximum, minimum, long.MaxValue);

        var min = Math.Max(minimum, long.MinValue + 1);
        return min + (long)(anon.AnyDouble(0, 1, distribution) * ((double)maximum - (double)min));
    }
}
