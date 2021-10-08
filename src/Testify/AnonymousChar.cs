namespace Testify;

/// <summary>
/// Defines anon methods for creating anonymous <see langword="char"/> values.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AnonymousChar
{
    private static readonly Range BasicLatinCharRange = new Range(0x20, 0x7f);
    private static readonly Range LatinSupplementRange = new Range(0xa0, 0xff);
    private static readonly Range LowerCaseAlphaRange = new Range('a', 'z');
    private static readonly Range NumericRange = new Range('0', '9');
    private static readonly Range UpperCaseAlphaRange = new Range('A', 'Z');

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of alpha characters using
    /// the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random alpha character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyAlphaChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);
        return (char)Range.CreateLongFromRanges(anon, distribution, LowerCaseAlphaRange, UpperCaseAlphaRange);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of alpha/numeric characters
    /// using the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random alpha/numeric character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyAlphaNumericChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return (char)Range.CreateLongFromRanges(anon, distribution, LowerCaseAlphaRange, UpperCaseAlphaRange, NumericRange);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of basic Latin characters using
    /// the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random basic Latin character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyBasicLatinChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return anon.AnyChar((char)BasicLatinCharRange.Minimum, (char)BasicLatinCharRange.Maximum, distribution);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the specified range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="char"/> value.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyChar(this IAnonymousData anon, char minimum = char.MinValue, char maximum = char.MaxValue, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);
        Guard.Against.OutOfRange(maximum, minimum, maximum);

        return (char)anon.AnyInt64(minimum, maximum, distribution);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of Latin supplement characters
    /// using the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random Latin supplement character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyLatinSupplementChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return anon.AnyChar((char)LatinSupplementRange.Minimum, (char)LatinSupplementRange.Maximum, distribution);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of numeric characters using
    /// the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random numeric character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyNumericChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return anon.AnyChar((char)NumericRange.Minimum, (char)NumericRange.Maximum, distribution);
    }

    /// <summary>
    /// Creates an anonymous <see langword="char"/> value within the range of printable characters
    /// using the specified distribution algorithm.
    /// </summary>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random printable character.</returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
    public static char AnyPrintableChar(this IAnonymousData anon, Distribution? distribution = default)
    {
        Guard.Against.Null(anon);

        return (char)Range.CreateLongFromRanges(anon, distribution, BasicLatinCharRange, LatinSupplementRange);
    }
}
