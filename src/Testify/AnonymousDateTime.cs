using System;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating anonymous <see langword="DateTime"/> values.
    /// </summary>
    // Temorary (hopefull) workaround for DocFX
    // [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousDateTime
    {
        /// <summary>
        /// Creates an anonymous <see langword="DateTime"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="DateTime"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static DateTime AnyDateTime(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDateTime(DateTime.MinValue, DateTime.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="DateTime"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="DateTime"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static DateTime AnyDateTime(this IAnonymousData anon, DateTime minimum, DateTime maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, DateTime.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyDateTime(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="DateTime"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="DateTime"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        public static DateTime AnyDateTime(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDateTime(DateTime.MinValue, DateTime.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="DateTime"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="DateTime"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static DateTime AnyDateTime(this IAnonymousData anon, DateTime minimum, DateTime maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, DateTime.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            long ticks = anon.AnyInt64(minimum.Ticks, maximum.Ticks, distribution);
            return new DateTime(ticks);
        }
    }
}