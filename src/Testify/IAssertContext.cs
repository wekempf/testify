namespace Testify;

/// <summary>
/// The assertion context used by assertion methods.
/// </summary>
/// <typeparam name="T">The type of the value being asserted on.</typeparam>
public interface IAssertContext<T>
{
    /// <summary>
    /// Gets the actual value being asserted on.
    /// </summary>
    T? ActualValue { get; }

    /// <summary>
    /// Gets the expression used for the <see cref="IAssertContext{T}.ActualValue"/>.
    /// </summary>
    string ActualExpression { get; }

    /// <summary>
    /// Gets a successful assertion result.
    /// </summary>
    AssertResult Success { get; }

    /// <summary>
    /// Gets a failed assertion result, or throws.
    /// </summary>
    /// <param name="message">The assertion failure message.</param>
    /// <returns>A failed assertion result.</returns>
    AssertResult Failure(string message);
}
