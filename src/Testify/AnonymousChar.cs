using System;
using System.ComponentModel;

namespace Testify
{
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
        /// a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random alpha character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyAlphaChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyAlphaChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of alpha characters using
        /// the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random alpha character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyAlphaChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return (char)Range.CreateLongFromRanges(anon, distribution, LowerCaseAlphaRange, UpperCaseAlphaRange);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of alpha/numeric characters
        /// using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="char"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyAlphaNumericChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyAlphaNumericChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of alpha/numeric characters
        /// using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random alpha/numeric character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyAlphaNumericChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return (char)Range.CreateLongFromRanges(anon, distribution, LowerCaseAlphaRange, UpperCaseAlphaRange, NumericRange);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of basic Latin characters using
        /// a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random basic Latin character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyBasicLatinChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyBasicLatinChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of basic Latin characters using
        /// the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random basic Latin character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyBasicLatinChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyChar((char)BasicLatinCharRange.Minimum, (char)BasicLatinCharRange.Maximum, distribution);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="char"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyChar(char.MinValue, char.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="char"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyChar(this IAnonymousData anon, char minimum, char maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyChar(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="char"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyChar(char.MinValue, char.MaxValue, distribution);
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
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyChar(this IAnonymousData anon, char minimum, char maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, char.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (char)anon.AnyInt64(minimum, maximum, distribution);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of Latin supplement characters
        /// using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random Latin supplement character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyLatinSupplementChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyLatinSupplementChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of Latin supplement characters
        /// using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random Latin supplement character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyLatinSupplementChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyChar((char)LatinSupplementRange.Minimum, (char)LatinSupplementRange.Maximum, distribution);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of numeric characters using
        /// a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random numeric character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyNumericChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyNumericChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of numeric characters using
        /// the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random numeric character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyNumericChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyChar((char)NumericRange.Minimum, (char)NumericRange.Maximum, distribution);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of printable characters
        /// using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random printable character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyPrintableChar(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyPrintableChar(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="char"/> value within the range of printable characters
        /// using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random printable character.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static char AnyPrintableChar(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return (char)Range.CreateLongFromRanges(anon, distribution, BasicLatinCharRange, LatinSupplementRange);
        }
    }
}