using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        public AnonymousDataException(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDataException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be created.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        public AnonymousDataException(Type type, Exception innerException)
            : base($"Unable to create instance of specified type {type}.", innerException)
        {
            Argument.NotNull(type, nameof(type));
            this.AnonymousType = type;
        }

        /// <summary>
        /// Gets the anonymous type that could not be created.
        /// </summary>
        public Type AnonymousType { get; }
    }
}
