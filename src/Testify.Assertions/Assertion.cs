namespace Testify;

using System.Text.RegularExpressions;

using Testify.Internal;

/// <summary>
/// Provides methods used to start an assertion or for use within an assertion
/// implementation.
/// </summary>
public static partial class Assertion
{
    [GeneratedRegex("{because}", RegexOptions.Compiled)]
    private static partial Regex BecauseHole();

    /// <summary>
    /// Begins a fluent assertion by providing the actual value being asserted
    /// on.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <param name="value">The actual value.</param>
    /// <param name="expression">The expression used when providing the actual
    ///     value.</param>
    /// <returns>An <see cref="ActualValue{T}"/> instance representing the
    ///     actual value being asserted on.</returns>
    public static ActualValue<T> Assert<T>(
        T? value,
        [Expression("value")] string? expression = null)
        => new(value, Guard.Against.Null(expression));

    /// <summary>
    /// Begins a fluent assertion by providing an <see cref="Action"/> as the
    /// value being asserted on.
    /// </summary>
    /// <param name="action">The <see cref="Action"/> for the actual value.
    ///     </param>
    /// <param name="expression">The expression used when providing the action.
    ///     </param>
    /// <returns>An <see cref="ActualValue{Action}"/> instance representing the
    ///     actual value being asserted on.</returns>
    public static ActualValue<Action> Assert(
        Action action,
        [Expression("action")] string? expression = null)
        => Assert<Action>(action, expression);

    /// <summary>
    /// Makes a "compound assertion" that fails with the specified message if any
    /// wrapped assertions fail.
    /// </summary>
    /// <param name="message">The assertion failure message to report if any
    ///     wrapped assertions fail.</param>
    /// <param name="assertions">The <see cref="Action"/> to invoke which makes
    ///     assertions to be wrapped in the "compound assertions".</param>
    /// <remarks>
    /// This is a very low level assertion generally used in the implementation
    /// of other "compound assertions" and not made directly within tests. The
    /// behavior of assertions are temporarily changed within the scope of the
    /// invoked <paramref name="assertions"/> to combine assertion failures,
    /// rather than to immediately throw them. This allows multiple assertions
    /// to be combined into a single assertion with a meaningful failure
    /// message. Note that only assertion methods within <b>Testify</b> will be
    /// combined this way, and any other exceptions thrown within the
    /// <paramref name="assertions"/> will cause an immediate test failure.
    /// </remarks>
    public static void Assert(string message, Action assertions)
    {
        AssertionScope.Push(message);
        try
        {
            assertions.Invoke();
        }
        catch
        {
            AssertionScope.Pop(false);
            throw;
        }

        AssertionScope.Pop();
    }

    /// <summary>
    /// Asserts that the <paramref name="actual"/> value should satisfy all of
    /// the assertions made when invoking <paramref name="assertions"/>.
    /// </summary>
    /// <typeparam name="T">The type of the actual value being asserted on.
    ///     </typeparam>
    /// <param name="actual">The actual value being asserted on.</param>
    /// <param name="assertions">An <see cref="Action{T}"/> that makes multiple
    ///     assertions on the <paramref name="actual"/> value.</param>
    public static void ShouldSatisfy<T>(this ActualValue<T> actual, Action<ActualValue<T>> assertions)
        => Assert("One or more assertions were not satisfied.", () => assertions.Invoke(actual));

    /// <summary>
    /// Generates a test platform specific failure exception. If the test
    /// platform cannot be determined then raises the non-platform specific
    /// <see cref="AssertionException"/>.
    /// </summary>
    /// <param name="message">The assertion message, including the "{because}"
    ///     hole used to format the user specified reason for the failure.
    ///     </param>
    /// <param name="because">The user specified reason for the failure.</param>
    /// <exception cref="Exception"></exception>
    public static void Fail(string message, string? because = null)
    {
        Guard.Against.NullOrWhiteSpace(message);

        if (!string.IsNullOrWhiteSpace(because))
        {
            because = because.Trim();
            if (!because.StartsWith("because"))
            {
                because = $"because {because}";
            }

            message = BecauseHole().Replace(message, " " + because);
        }

        AssertionScope.Fail(message);
    }
}
