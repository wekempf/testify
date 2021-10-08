namespace Testify.Internal;

/// <summary>
/// Assertion failure exception used when a test framework cannot be detected.
/// </summary>
[Serializable]
internal class AssertFailedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssertFailedException"/> class.
    /// </summary>
    public AssertFailedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertFailedException"/> class with the specified message.
    /// </summary>
    /// <param name="message">The assertion failure message.</param>
    public AssertFailedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertFailedException"/> class with the specified message and inner exception.
    /// </summary>
    /// <param name="message">The assertion failure message.</param>
    /// <param name="innerException">The inner exception.</param>
    public AssertFailedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertFailedException"/> class with the specified serialization info.
    /// </summary>
    /// <param name="info">The serialization info.</param>
    /// <param name="context">The serialization streaming context.</param>
    protected AssertFailedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}