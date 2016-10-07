using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

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
        /// <typeparam name="TInstance">The type of the instance to populate.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="instance">The instance to populate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is null.</exception>
        /// <returns>The populated instance.</returns>
        public static TInstance Populate<TInstance>(this IAnonymousData anon, TInstance instance)
        {
            Argument.NotNull(anon, nameof(anon));

            return anon.Populate(instance, false);
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

        /// <summary>
        /// Registers a factory method for the specified property.
        /// </summary>
        /// <typeparam name="T">The type that declares the property.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="propertyExpression">An expression representing the property to populate.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> or <paramref name="propertyExpression"/> or
        /// <paramref name="factory"/> is <c>null</c>.</exception>
        public static void Register<T, TProperty>(
            this IRegisterAnonymousData anon,
            Expression<Func<T, TProperty>> propertyExpression,
            Func<IAnonymousData, TProperty> factory)
        {
            Argument.NotNull(anon, nameof(anon));
            Argument.NotNull(propertyExpression, nameof(propertyExpression));
            Argument.NotNull(factory, nameof(factory));

            var member = ReflectionExtensions.GetMemberInfo(propertyExpression);
            var property = member?.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Invalid property expression.");
            }

            anon.Register(property, f => factory(f));
        }
    }
}