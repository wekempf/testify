namespace Testify;

/// <summary>
/// Extension methods for anonymous <see cref="long"/> type.
/// </summary>
public static class AnonymousDouble
{
    /// <summary>
    /// Creates a random <see langword="double"/> value within the specified negative range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="double"/> value.</returns>
    public static double AnyNegativeDouble(this IAnonymousData anon, double minimum = double.MinValue, Distribution? distribution = default)
        => anon.AnyDouble(Guard.Against.OutOfRange(minimum, double.MinValue, -1), -1, distribution);

    /// <summary>
    /// Creates a random <see langword="double"/> value within the specified positive range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="double"/> value.</returns>
    public static double AnyPositiveDouble(this IAnonymousData anon, double maximum = double.MaxValue, Distribution? distribution = default)
        => anon.AnyDouble(0, Guard.Against.OutOfRange(maximum, 0, double.MaxValue), distribution);
}
