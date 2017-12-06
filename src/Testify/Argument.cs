using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Testify
{
    /// <summary>
    /// Provides argument validation methods.
    /// </summary>
    internal static class Argument
    {
        /// <summary>
        /// Ensures the argument is in the specified range.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <param name="minimum">The minimum allowable value.</param>
        /// <param name="maximum">The maximum allowable value.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException">The argument was not in the specified range.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void InRange<T>(T value, T minimum, T maximum, string argName)
            where T : IComparable<T>
        {
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(value, minimum) < 0 || comparer.Compare(value, maximum) > 0)
            {
                throw new ArgumentOutOfRangeException(argName);
            }
        }

        /// <summary>
        /// Ensures the argument is in the specified range.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <param name="minimum">The minimum allowable value.</param>
        /// <param name="maximum">The maximum allowable value.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">The argument was not in the specified range.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void InRange<T>(T value, T minimum, T maximum, string argName, string message)
            where T : IComparable<T>
        {
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(value, minimum) < 0 || comparer.Compare(value, maximum) > 0)
            {
                throw new ArgumentOutOfRangeException(argName, message);
            }
        }

        /// <summary>
        /// Ensures the argument is assignable to the specified type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type <paramref name="value"/> should be assignable to.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <exception cref="ArgumentException">The <paramref name="value"/> is not assignable to the <paramref name="type"/>.</exception>
        internal static void IsAssignableTo(object value, Type type, string argName)
        {
            if (!type.IsInstanceOfType(value))
            {
                throw new ArgumentException($"Value must be of type {type}", argName);
            }
        }

        /// <summary>
        /// Ensures the argument is an <see cref="Enum"/> type.
        /// </summary>
        /// <param name="value">The argument value.</param>
        /// <param name="paramName">The argument name.</param>
        /// <exception cref="ArgumentException">The <paramref name="value"/> was not an <see cref="Enum"/>
        /// type.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void IsEnumType(Type value, string paramName)
        {
            if (!value.IsEnum())
            {
                throw new ArgumentException(paramName, "Type must be an Enum type.");
            }
        }

        /// <summary>
        /// Ensures the argument is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <param name="argName">The argument name.</param>
        /// <exception cref="ArgumentNullException">The argument was <c>null</c>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void NotNull<T>(T value, string argName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Ensures the argument is not <c>null</c> or <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="value">The argument value.</param>
        /// <param name="argName">The argument name.</param>
        /// <exception cref="ArgumentNullException">The argument was <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The argument was <see cref="string.Empty"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void NotNullOrEmpty(string value, string argName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argName);
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The string may not be empty.", argName);
            }
        }
    }
}