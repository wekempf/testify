using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="TimeSpan"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousTimeSpan
    {
        /// <summary>
        /// Creates a random <see langword="TimeSpan"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static TimeSpan AnyTimeSpan(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="TimeSpan"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static TimeSpan AnyTimeSpan(this IAnonymousData anon, TimeSpan minimum, TimeSpan maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, TimeSpan.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyTimeSpan(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="TimeSpan"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static TimeSpan AnyTimeSpan(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="TimeSpan"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static TimeSpan AnyTimeSpan(this IAnonymousData anon, TimeSpan minimum, TimeSpan maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, TimeSpan.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            var ticks = anon.AnyInt64(minimum.Ticks, maximum.Ticks, distribution);
            return new TimeSpan(ticks);
        }
    }
}