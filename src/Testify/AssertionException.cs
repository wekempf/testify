using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Testify.Properties;

namespace Testify
{
    /// <summary>
    /// Represents one or more assertion errors.
    /// </summary>
    [DebuggerDisplay("Count = {InnerExceptionCount}")]
    public class AssertionException : AggregateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        public AssertionException()
            : base(Resources.AssertionException_UnspecifiedAssertionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AssertionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="innerException"/> argument is null.</exception>
        public AssertionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is null.</exception>
        /// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is null.</exception>
        public AssertionException(params Exception[] innerExceptions)
            : base(Resources.AssertionException_UnspecifiedAssertionMessage, innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is null.</exception>
        /// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is null.</exception>
        public AssertionException(string message, params Exception[] innerExceptions)
            : base(message, innerExceptions)
        {
        }

        [SuppressMessage("SonarLint", "S1144", Justification = "This is used by DebuggerDisplay.")]
        private int InnerExceptionCount => InnerExceptions.Count;
    }
}