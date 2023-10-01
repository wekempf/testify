namespace Testify;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Represents assertion failures that occur during test execution.
/// </summary>
/// <remarks>
/// This is the exception type thrown by assertions in the <b>Testify</b>
/// framework when no unit test framework can be detected.
/// </remarks>
[Serializable]
public class AssertionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionException"/>
    /// class.
    /// </summary>
    public AssertionException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionException"/> class
    /// with a specified failure message.
    /// </summary>
    /// <param name="message">The message that describes the reason for an
    ///     assertion failure.</param>
    public AssertionException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionException"/> class
    /// with a specified failure message and a reference to the inner exception
    /// that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the reason for an
    ///     assertion failure.</param>
    /// <param name="inner">The exception that is the cause of the current
    ///     exception, or a <see langword="null"/> reference if no inner
    ///     exception is specified.</param>
    public AssertionException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionException"/> class
    /// with serialized data.
    /// </summary>
    /// <param name="info">The
    ///     <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that
    ///     holds the serialized object data about the exception being thrown.
    ///     </param>
    /// <param name="context">The
    ///     <see cref="T:System.Runtime.Serialization.StreamingContext"/> that
    ///     contains contextual information about the source or destination.
    ///     </param>
    /// <exception cref="ArgumentNullException"><paramref name="info"/> is
    ///     <see langword="null"/>.</exception>
    /// <exception cref="SerializationException">The class name is
    ///     <see langword="null"/> or <see cref="Exception.HResult"/> is zero
    ///     (0).</exception>
    protected AssertionException(
      SerializationInfo info,
      StreamingContext context) : base(info, context) { }
}