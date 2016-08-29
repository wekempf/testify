using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="int"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousInt32
    {
        /// <summary>
        /// Creates a random <see langword="int"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static int AnyInt32(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(int.MinValue, int.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="int"/> value within the specified range using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static int AnyInt32(this IAnonymousData anon, int minimum, int maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, int.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyInt32(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="int"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static int AnyInt32(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(int.MinValue, int.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="int"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static int AnyInt32(this IAnonymousData anon, int minimum, int maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, int.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (int)anon.AnyInt64(minimum, maximum, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="int"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static int AnyPositiveInt32(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(0, int.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="int"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static int AnyPositiveInt32(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(0, int.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="int"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static int AnyNegativeInt32(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(int.MinValue, -1, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="int"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="int"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static int AnyNegativeInt32(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt32(int.MinValue, -1, distribution);
        }
    }
}