namespace Testify
{
    /// <summary>
    /// Nullable type extensions.
    /// </summary>
    internal static class NullableExtensions
    {
        /// <summary>
        /// Turns a non-nullable into a nullable.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The value as a nullable type.</returns>
        internal static T? AsNullable<T>(this T value)
            where T : struct
            => new T?(value);
    }
}