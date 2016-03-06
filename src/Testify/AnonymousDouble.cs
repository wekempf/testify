using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines anon methods for creating <see langword="double"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousDouble
    {
        /// <summary>
        /// Creates a random <see langword="double"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static double AnyDouble(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(double.MinValue, double.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="double"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static double AnyDouble(this IAnonymousData anon, double minimum, double maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyDouble(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="double"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static double AnyDouble(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(double.MinValue, double.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static double AnyPositiveDouble(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(0, double.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static double AnyPositiveDouble(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(0, double.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static double AnyNegativeDouble(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(double.MinValue, 0, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static double AnyNegativeDouble(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(double.MinValue, 0, distribution);
        }
    }
}