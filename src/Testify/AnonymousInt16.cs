using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="short"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousInt16
    {
        /// <summary>
        /// Creates a random <see langword="short"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static short AnyInt16(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(short.MinValue, short.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="short"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static short AnyInt16(this IAnonymousData anon, short minimum, short maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, short.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyInt16(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="short"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static short AnyInt16(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(short.MinValue, short.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="short"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static short AnyInt16(this IAnonymousData anon, short minimum, short maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, short.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (short)anon.AnyInt64(minimum, maximum, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="short"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static short AnyNegativeInt16(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(short.MinValue, -1, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="short"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static short AnyNegativeInt16(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(short.MinValue, -1, distribution);
        }

        /// <summary>
        /// Creates a random positive <see langword="short"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random positive <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static short AnyPositiveInt16(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(0, short.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random positive <see langword="short"/> value.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random positive <see langword="short"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method may return a zero value, which strictly makes this "any non-negative" from a mathematical
        /// perspective, but the term "positive" is used because this is what many would expect.
        /// </remarks>
        public static short AnyPositiveInt16(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyInt16(0, short.MaxValue, distribution);
        }
    }
}