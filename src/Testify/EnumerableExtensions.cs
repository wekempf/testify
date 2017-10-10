using System;
using System.Collections.Generic;
using System.Text;

namespace Testify
{
    internal static class EnumerableExtensions
    {
        public static T OnlyOrDefault<T>(this IEnumerable<T> source)
        {
            Argument.NotNull(source, nameof(source));

            bool found = false;
            T result = default(T);
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
