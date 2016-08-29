using System;
using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Extension methods for <see cref="AnonymousData"/> use.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousDataExtensions
    {
        /// <summary>
        /// Creates an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to create.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        public static T Any<T>(this IAnonymousData anon)
        {
            Argument.NotNull(anon, nameof(anon));

            return (T)anon.Any(typeof(T));
        }

        /// <summary>
        /// Creates an object of the specified type and optionally populates the properties.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="type">The type to create..</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="type"/>
        /// is null.</exception>
        public static object Any(this IAnonymousData anon, Type type)
        {
            Argument.NotNull(anon, nameof(anon));
            return anon.Any(type, PopulateOption.None);
        }

        /// <summary>
        /// Anies the specified populate option.
        /// </summary>
        /// <typeparam name="T">The type of the object to create.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="populateOption">Specifies how to populate the properties.</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        public static T Any<T>(this IAnonymousData anon, PopulateOption populateOption)
        {
            Argument.NotNull(anon, nameof(anon));

            return (T)anon.Any(typeof(T), populateOption);
        }

        /// <summary>
        /// Freezes the specified value as the result for any further calls to <see cref="Any"/>
        /// for the specified type.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="type">The type to freeze.</param>
        /// <param name="value">The instance to freeze.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="type"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not assignable to <paramref name="type"/>.</exception>
        public static void Freeze(this IRegisterAnonymousData anon, Type type, object value)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(type, nameof(type));
            Argument.IsAssignableTo(value, type, nameof(value));

            anon.Register(type, f => value);
        }

        /// <summary>
        /// Freezes the specified value as the result for any further calls to <see cref="Any"/>
        /// for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to freeze.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="value">The instance to freeze.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <c>null</c>.</exception>
        public static void Freeze<T>(this IRegisterAnonymousData anon, T value)
        {
            Argument.NotNull(anon, nameof(anon));

            anon.Freeze(typeof(T), value);
        }

        /// <summary>
        /// Populates the specified instance by assigning all properties to anonymous values.
        /// </summary>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="instance">The instance to populate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        public static void Populate(this IAnonymousData anon, object instance)
        {
            Argument.NotNull(anon, nameof(anon));

            anon.Populate(instance, true);
        }

        /// <summary>
        /// Register a factory method for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object the factory method creates.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="factory"/> is <c>null</c>.</exception>
        public static void Register<T>(this IRegisterAnonymousData anon, Func<IAnonymousData, T> factory)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(factory, nameof(factory));

            anon.Register(typeof(T), f => factory(f));
        }
    }
}