using System.Collections.Generic;

namespace Testify
{
    /// <summary>
    /// Enumerable extensions.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the only item in the collection or the default value if there's no items or more than one item.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The only item or default(T) if there's no items or more than one item.</returns>
        internal static T OnlyOrDefault<T>(this IEnumerable<T> source)
        {
            Argument.NotNull(source, nameof(source));

            var found = false;
            var result = default(T);
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (found)
                    {
                        return default(T);
                    }

                    found = true;
                    result = e.Current;
                }

                return found ? result : default(T);
            }
        }
    }
}