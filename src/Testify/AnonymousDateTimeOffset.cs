using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="DateTimeOffset"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousDateTimeOffset
    {
        /// <summary>
        /// Creates a random <see langword="DateTimeOffset"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static DateTimeOffset AnyDateTimeOffset(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="DateTimeOffset"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static DateTimeOffset AnyDateTimeOffset(this IAnonymousData anon, DateTimeOffset minimum, DateTimeOffset maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, DateTimeOffset.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyDateTimeOffset(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random <see langword="DateTimeOffset"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static DateTimeOffset AnyDateTimeOffset(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue, distribution);
        }

        /// <summary>
        /// Creates a random <see langword="DateTimeOffset"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static DateTimeOffset AnyDateTimeOffset(this IAnonymousData anon, DateTimeOffset minimum, DateTimeOffset maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, DateTimeOffset.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            var timeZone = anon.AnyTimeZoneInfo(distribution);
            var ticks = anon.AnyInt64(minimum.Ticks, maximum.Ticks, distribution);
            return new DateTimeOffset(ticks, timeZone.BaseUtcOffset);
        }
    }
}