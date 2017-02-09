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
            : this(type, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public AnonymousDataException(Type type, Exception innerException)
            : base($"Unable to create instance of specified type {type}.", innerException)
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
    }
}