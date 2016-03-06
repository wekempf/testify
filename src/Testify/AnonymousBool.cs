using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Provides anon methods for creating anonymous <see cref="bool"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousBool
    {
        /// <summary>
        /// Creates an anonymous <see langword="bool"/> value using a uniform distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="bool"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static bool AnyBool(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyBool(Distribution.Uniform);
        }

        /// <summary>
        /// Creates an anonymous <see langword="bool"/> value using the specified distribution algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="bool"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static bool AnyBool(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyDouble(0, 1, distribution) >= 0.5;
        }
    }
}