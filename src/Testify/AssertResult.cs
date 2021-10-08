namespace Testify;

/// <summary>
/// Represents the results of an assertion.
/// </summary>
public class AssertResult
{
    private static readonly AssertResult SuccessResult = new AssertResult(null);

    private AssertResult(string? failureMessage) => FailureMessage = failureMessage;

    /// <summary>
    /// Gets a value indicating whether or not this represents a failed assertion.
    /// </summary>
    public bool IsFailure => FailureMessage != null;

    /// <summary>
    /// Gets the failure message for a failed assertion.
    /// </summary>
    public string? FailureMessage { get; }

    /// <summary>
    /// Creates a success <see cref="AssertResult"/>.
    /// </summary>
    /// <returns>The successful assertion result.</returns>
    public static AssertResult Success() => SuccessResult;

    /// <summary>
    /// Creates a failure <see cref="AssertResult"/>.
    /// </summary>
    /// <param name="failureMessage">The failure message associated with the <see cref="AssertResult"/>.</param>
    /// <returns>A failure <see cref="AssertResult"/> with the specified failure message.</returns>
    public static AssertResult Failure(string failureMessage)
    {
        ArgumentNullException.ThrowIfNull(failureMessage);
        return new AssertResult(failureMessage);
    }
}
