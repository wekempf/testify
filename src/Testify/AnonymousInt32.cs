namespace Testify;

/// <summary>
/// Extension methods for anonymous <see cref="int"/> type.
/// </summary>
public static class AnonymousInt32
{
    /// <summary>
    /// Creates a random <see langword="int"/> value within the specified range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="long"/> value.</returns>
    public static int AnyInt32(this IAnonymousData anon, int minimum = int.MinValue, int maximum = int.MaxValue, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);
        Guard.Against.OutOfRange(maximum, minimum, int.MaxValue);

        var min = Math.Max(minimum, int.MinValue + 1);
        return min + (int)(anon.AnyDouble(0, 1, distribution) * ((double)maximum - (double)min));
    }
}
