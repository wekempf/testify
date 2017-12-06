using System;
using System.Collections.Generic;
using System.Text;

namespace Testify
{
    internal static class NullableExtensions
    {
        public static Nullable<T> AsNullable<T>(this T value) where T : struct
            => new Nullable<T>(value);
    }
}
