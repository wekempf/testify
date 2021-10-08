namespace Testify;

/// <summary>
/// Delayed variant of the <see cref="IAssertContext{T}"/>.
/// </summary>
/// <typeparam name="T">The type of actual value.</typeparam>
internal class VerifyContext<T> : IAssertContext<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VerifyContext{T}"/> class.
    /// </summary>
    /// <param name="actualValue">The actual value.</param>
    /// <param name="actualExpression">The expression used for the actual value.</param>
    public VerifyContext(T? actualValue, string actualExpression)
        => (ActualValue, ActualExpression) = (actualValue, actualExpression);

    /// <inheritdoc/>
    public T? ActualValue { get; }

    /// <inheritdoc/>
    public string ActualExpression { get; }

    /// <inheritdoc/>
    public AssertResult Success => AssertResult.Success();

    /// <inheritdoc/>
    public AssertResult Failure(string failureMessage) => AssertResult.Failure(failureMessage);
}
