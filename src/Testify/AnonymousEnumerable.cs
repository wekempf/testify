using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Testify
{
    /// <summary>
    /// Defines factory methods for creating <see langword="IEnumerable"/> values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousEnumerable
    {
        /// <summary>
        /// Creates a random <see langword="IEnumerable"/> sequence of 0 to 20 objects of the specified type.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="type">The type of objects to create.</param>
        /// <returns>A random <see langword="IEnumerable"/> sequence of the specified type of objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="type"/> is <see langword="null"/>.</exception>
        public static IEnumerable AnyEnumerable(this IAnonymousData anon, Type type)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(type, nameof(type));

            return anon.AnyEnumerable(type, 0, 20);
        }

        /// <summary>
        /// Creates a random <see langword="IEnumerable"/> sequence of objects of the specified type.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="type">The type of objects to create.</param>
        /// <param name="minimumLength">The minimum length of the sequence.</param>
        /// <param name="maximumLength">The maximum length of the sequence.</param>
        /// <returns>A random <see langword="IEnumerable"/> sequence of the specified type of objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumLength"/> is less than <paramref name="minimumLength"/>.</exception>
        public static IEnumerable AnyEnumerable(this IAnonymousData anon, Type type, int minimumLength, int maximumLength)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(type, nameof(type));
            Argument.InRange(maximumLength, minimumLength, int.MaxValue, nameof(maximumLength), "The maximum length must be greater than the minimum length.");

            int length = anon.AnyInt32(minimumLength, maximumLength);
            return AnyEnumerable(anon, type, length);
        }

        /// <summary>
        /// Creates a random <see langword="IEnumerable{T}"/> sequence of 0 to 20 objects of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to create.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random <see langword="IEnumerable{T}"/> sequence of the specified type of objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> AnyEnumerable<T>(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.AnyEnumerable(typeof(T), 0, 20).Cast<T>();
        }

        /// <summary>
        /// Creates a random <see langword="IEnumerable{T}"/> sequence of objects of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to create.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="minimumLength">The minimum length of the sequence.</param>
        /// <param name="maximumLength">The maximum length of the sequence.</param>
        /// <returns>A random <see langword="IEnumerable{T}"/> sequence of the specified type of objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumLength"/> is less than <paramref name="minimumLength"/>.</exception>
        public static IEnumerable<T> AnyEnumerable<T>(this IAnonymousData anon, int minimumLength, int maximumLength)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.InRange(maximumLength, minimumLength, int.MaxValue, nameof(maximumLength), "The maximum length must be greater than the minimum length.");

            return anon.AnyEnumerable(typeof(T), minimumLength, maximumLength).Cast<T>();
        }

        /// <summary>
        /// Returns a random item from the specified collection.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>A random item from the collection.</returns>
        public static T AnyItem<T>(this IAnonymousData anon, IEnumerable<T> collection)
        {
            var items = collection as IList<T> ?? collection.ToList();
            var index = anon.AnyInt32(0, items.Count - 1);
            return items[index];
        }

        private static IEnumerable AnyEnumerable(IAnonymousData anon, Type type, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                yield return anon.Any(type);
            }
        }
    }
}