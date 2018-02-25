using System.Collections.Generic;
using System.Linq;

namespace Testify
{
    /// <summary>
    /// Provides factory methods for creating anonymous arrays.
    /// </summary>
    /// <typeparam name="T">The array's element type.</typeparam>
    internal static class AnonymousArray<T>
    {
        /// <summary>
        /// Creates an anonymous array.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use when creating items.</param>
        /// <returns>A new anonymous array.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
        internal static T[] AnyArray(IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return AnyEnumerable(anon).ToArray();
        }

        /// <summary>
        /// Creates an anonymous sequence of items.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use when creating items.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence of new items.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
        internal static IEnumerable<T> AnyEnumerable(IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyEnumerable<T>();
        }
    }
}