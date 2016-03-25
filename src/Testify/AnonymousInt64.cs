using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="long"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousInt64
    {
        /// <summary>
        /// Creates a random <see langword="long"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static long AnyInt64(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(long.MinValue, long.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="long"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static long AnyInt64(this IAnonymousData anon, long minimum, long maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, long.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyInt64(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="long"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static long AnyInt64(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(long.MinValue, long.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="long"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static long AnyInt64(this IAnonymousData anon, long minimum, long maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, long.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            var min = Math.Max(minimum, long.MinValue + 1);
            return min + (long)(anon.AnyDouble(0, 1, distribution) * ((double)maximum - (double)min));
        }

        /// <summary>
        /// Creates a random positive <see langword="long"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static long AnyPositiveInt64(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(0, long.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="long"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static long AnyPositiveInt64(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(0, long.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="long"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static long AnyNegativeInt64(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(long.MinValue, -1, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="long"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="long"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static long AnyNegativeInt64(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt64(long.MinValue, -1, distribution);
        }
    }
}