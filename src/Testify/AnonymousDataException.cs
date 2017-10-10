using System;
using System.Reflection;

namespace Testify
{
    /// <summary>
    /// The exception that is thrown if <see cref="IAnonymousData"/> is unable to create
    /// an instance of the specified type.
    /// </summary>
    public class AnonymousDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public AnonymousDataException(Type type)
            : this(type, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public AnonymousDataException(Type type, Exception innerException)
            : this(type, null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <param name="message">The extra message to include in the <see cref="Exception.Message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public AnonymousDataException(Type type, string message)
            : this(type, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <param name="message">The extra message to include in the <see cref="Exception.Message"/>.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public AnonymousDataException(Type type, string message, Exception innerException)
            : base(GetMessage(type, message), innerException)
        {
            Argument.NotNull(type, nameof(type));
            AnonymousType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="property">The property that could not be populated.</param>
        public AnonymousDataException(PropertyInfo property)
            : this(property, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="property">The property that could not be populated.</param>
        /// <param name="innerException">The inner exception.</param>
        public AnonymousDataException(PropertyInfo property, Exception innerException)
            : base($"Unable to populate property {property?.Name} on type {property?.DeclaringType}.", innerException)
        {
            Argument.NotNull(property, nameof(property));
            AnonymousType = property.DeclaringType;
        }

        /// <summary>
        /// Gets the anonymous type that could not be created.
        /// </summary>
        public Type AnonymousType { get; }

        private static string GetMessage(Type type, string message)
        {
            var result = $"Unable to create instance of specified type {type}.";
            if (!string.IsNullOrEmpty(message))
            {
                result += " " + message;
            }

            return result;
        }
    }
}