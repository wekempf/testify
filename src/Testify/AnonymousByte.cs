using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Defines anon methods for creating <see langword="int"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousByte
    {
        /// <summary>
        /// Creates an anonymous <see langword="byte"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="byte"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static byte AnyByte(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyByte(byte.MinValue, byte.MaxValue, Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="byte"/> value within the specified range using a uniform
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <returns>A random <see langword="byte"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static byte AnyByte(this IAnonymousData anon, byte minimum, byte maximum)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return anon.AnyByte(minimum, maximum, Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="byte"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="byte"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static byte AnyByte(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyByte(byte.MinValue, byte.MaxValue, distribution);
        }

        /// <summary>
        /// Creates an anonymous <see langword="byte"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="byte"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public static byte AnyByte(this IAnonymousData anon, byte minimum, byte maximum, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

            return (byte)anon.AnyInt64(minimum, maximum, distribution);
        }
    }
}