using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines anon methods for creating <see langword="decimal"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousDecimal
    {
        /// <summary>
        /// Creates a random <see langword="decimal"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="decimal"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyDecimal(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDecimal(decimal.MinValue, decimal.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="decimal"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="decimal"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyDecimal(this IAnonymousData anon, decimal minimum, decimal maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, decimal.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (decimal)anon.AnyDecimal(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="decimal"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="decimal"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyDecimal(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return (decimal)anon.AnyDecimal(decimal.MinValue, decimal.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="decimal"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="decimal"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyDecimal(this IAnonymousData anon, decimal minimum, decimal maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, decimal.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (decimal)anon.AnyDouble((double)minimum, (double)maximum, distribution);
        }

        /// <summary>
        /// Creates a random negative <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random negative <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyNegativeDecimal(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDecimal(decimal.MinValue, 0, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random negative <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random negative <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static decimal AnyNegativeDecimal(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDecimal(decimal.MinValue, 0, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static decimal AnyPositiveDecimal(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDecimal(0, decimal.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="double"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="double"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static decimal AnyPositiveDecimal(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDecimal(0, decimal.MaxValue, distribution);
        }
    }
}