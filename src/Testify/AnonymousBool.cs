namespace Testify;

/// <summary>
/// Provides factory methods for creating anonymous <see cref="bool"/> values.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AnonymousBool
{
    /// <summary>
    /// Creates an anonymous <see cref="bool"/> value using the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see cref="bool"/> value.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static bool AnyBool(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return anon.AnyDouble(0, 1, distribution ?? Distribution.Uniform) >= 0.5;
    }
}
