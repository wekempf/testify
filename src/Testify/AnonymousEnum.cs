using System;

namespace Testify
{
    /// <summary>
    /// Defines anon methods for creating <see langword="Enum"/> values.
    /// </summary>
    // Temorary (hopefull) workaround for DocFX
    // [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousEnum
    {
        /// <summary>
        /// Creates a random value of the specified <see cref="Enum"/> type using a uniform distribution
        /// algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="enumType">The <see cref="Enum"/> type.</param>
        /// <returns>A random value of the specified <see cref="Enum"/> type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="enumType"/> is <c>null</c>.</exception>
        public static object AnyEnumValue(this IAnonymousData anon, Type enumType)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(enumType, nameof(enumType));
            Argument.IsEnumType(enumType, nameof(enumType));

            return anon.AnyEnumValue(enumType, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random value of the specified <see cref="Enum"/> type using the specified distribution
        /// algorithm.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="enumType">The <see cref="Enum"/> type.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random value of the specified <see cref="Enum"/> type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/>, <paramref name="enumType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="enumType"/> is not an enum type.</exception>
        public static object AnyEnumValue(this IAnonymousData anon, Type enumType, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(enumType, nameof(enumType));
            Argument.IsEnumType(enumType, nameof(enumType));

            var values = Enum.GetValues(enumType);
            var index = anon.AnyInt32(0, values.Length - 1, distribution);
            return values.GetValue(index);
        }

        /// <summary>
        /// Creates a random value of the specified <see cref="Enum"/> type using a uniform distribution
        /// algorithm.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/> type.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>A random value of the specified <see cref="Enum"/> type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static T AnyEnumValue<T>(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            var type = typeof(T);
            if (!type.IsEnum())
            {
                throw new InvalidOperationException("Generic parameter T must be an Enum type.");
            }

            return (T)anon.AnyEnumValue(type, Distribution.Uniform);
        }

        /// <summary>
        /// Creates a random value of the specified <see cref="Enum"/> type using the specified distribution
        /// algorithm.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/> type</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random value of the specified <see cref="Enum"/> type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static T AnyEnumValue<T>(this IAnonymousData anon, Distribution distribution)
        {
            Argument.NotNull(anon, nameof(anon));

            var type = typeof(T);
            if (!type.IsEnum())
            {
                throw new InvalidOperationException("Generic parameter T must be an Enum type.");
            }

            return (T)anon.AnyEnumValue(type, distribution);
        }
    }
}