namespace Testify;

/// <summary>
/// Provides the root methods for declaring assertion clauses.
/// </summary>
[EditorBrowsableAttribute(EditorBrowsableState.Never)]
public static class Assertions
{
    /// <summary>
    /// Starts an assertion clause that throws immediately on failure.
    /// </summary>
    /// <typeparam name="T">The type of the value being asserted on.</typeparam>
    /// <param name="actualValue">The actual value being asserted on.</param>
    /// <param name="actualExpression">The expression used for the actual value. If not provided the compiler provides this from the source code for the expression used for the <paramref name="actualValue"/> parameter.</param>
    /// <returns>The <see cref="IAssertContext{T}"/> that assertions use.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="actualExpression"/> was <see langword="null"/>.</exception>
    public static IAssertContext<T> Assert<T>(T? actualValue, [CallerArgumentExpression("actualValue")] string? actualExpression = default)
    {
        Guard.Against.Null(actualExpression);
        return new AssertContext<T>(actualValue, actualExpression);
    }

    /// <summary>
    /// Starts an assertion clause that throws immediately on failure.
    /// </summary>
    /// <param name="actualValue">The <see cref="Action"/> that will be invoked to get exception results.</param>
    /// <param name="actualExpression">The expression used for the actual value. If not provided the compiler provides this from the source code for the expression used for the <paramref name="actualValue"/> parameter.</param>
    /// <returns>The <see cref="IAssertContext{T}"/> that assertions use.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="actualExpression"/> was <see langword="null"/>.</exception>
    public static IAssertContext<Action> Assert(Action actualValue, [CallerArgumentExpression("actualValue")] string? actualExpression = default)
    {
        Guard.Against.Null(actualValue);
        Guard.Against.Null(actualExpression);

        return new AssertContext<Action>(actualValue, actualExpression);
    }

    /// <summary>
    /// Starts an assertion clause that throws immediately on failure.
    /// </summary>
    /// <typeparam name="T">The <see cref="Func{TResult}"/> result type.</typeparam>
    /// <param name="actualValue">The <see cref="Action"/> that will be invoked to get exception results.</param>
    /// <param name="actualExpression">The expression used for the actual value. If not provided the compiler provides this from the source code for the expression used for the <paramref name="actualValue"/> parameter.</param>
    /// <returns>The <see cref="IAssertContext{T}"/> that assertions use.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="actualExpression"/> was <see langword="null"/>.</exception>
    public static IAssertContext<Action> Assert<T>(Func<T> actualValue, [CallerArgumentExpression("actualValue")] string? actualExpression = default)
    {
        Guard.Against.Null(actualValue);
        Guard.Against.Null(actualExpression);

        Action action = () => actualValue.Invoke();
        return new AssertContext<Action>(action, actualExpression);
    }

    /// <summary>
    /// Throws if any assertions from Verify clauses failed.
    /// </summary>
    /// <param name="results">The results of an Verify assertions.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="results"/> is <see langword="null"/>.</exception>
    public static void AssertAll(params AssertResult[] results)
    {
        Guard.Against.Null(results);
        var result = VerifyAll(results);
        if (result.IsFailure)
        {
            throw FrameworkAdapter.CreateException(result.FailureMessage!);
        }
    }

    /// <summary>
    /// Starts an assertion clause that throws immediately on failure.
    /// </summary>
    /// <typeparam name="T">The type of the value being asserted on.</typeparam>
    /// <param name="actualValue">The actual value being asserted on.</param>
    /// <param name="actualExpression">The expression used for the actual value. If not provided the compiler provides this from the source code for the expression used for the <paramref name="actualValue"/> parameter.</param>
    /// <returns>The <see cref="IAssertContext{T}"/> that assertions use.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="actualExpression"/> was <see langword="null"/>.</exception>
    public static IAssertContext<T> Verify<T>(T? actualValue, [CallerArgumentExpression("actualValue")] string? actualExpression = default)
    {
        ArgumentNullException.ThrowIfNull(actualExpression);
        return new VerifyContext<T>(actualValue, actualExpression);
    }

    /// <summary>
    /// Returns a failure result if any assertions from Verify clauses failed.
    /// </summary>
    /// <param name="results">The results of an Verify assertions.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="results"/> is <see langword="null"/>.</exception>
    /// <returns>The overall assertion result.</returns>
    public static AssertResult VerifyAll(params AssertResult[] results)
    {
        ArgumentNullException.ThrowIfNull(results);
        StringBuilder? builder = null;
        foreach (var failure in results.Where(r => r.IsFailure))
        {
            builder ??= new StringBuilder("One or more assertions in a compound assertion failed.");
            builder.AppendLine();
            IndentFailure(failure, builder);
        }

        return builder != null ? AssertResult.Failure(builder.ToString()) : AssertResult.Success();
    }

    /// <summary>
    /// Fails immediately with an exception with the specified message.
    /// </summary>
    /// <param name="failureMessage">The failure message.</param>
    public static void Fail(string failureMessage)
    {
        ArgumentNullException.ThrowIfNull(failureMessage);
        throw FrameworkAdapter.CreateException(failureMessage);
    }

    /// <summary>
    /// Indents a failure result.
    /// </summary>
    /// <param name="result">The failure result.</param>
    /// <param name="builder">The <see cref="StringBuilder"/>.</param>
    internal static void IndentFailure(AssertResult result, StringBuilder builder)
    {
        Guard.Against.Null(result);
        Guard.Against.Null(builder);

        var lines = result.FailureMessage!.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        builder.Append($" * {lines[0]}");
        foreach (var line in lines.Skip(1))
        {
            builder.AppendLine();
            builder.Append($"   {line}");
        }
    }

    /// <summary>
    /// Indents a failure result.
    /// </summary>
    /// <param name="result">The failure result.</param>
    /// <returns>Indented failure message.</returns>
    internal static Text IndentFailure(AssertResult result)
    {
        var builder = new StringBuilder();
        IndentFailure(result, builder);
        return new Text(builder.ToString());
    }

    /// <summary>
    /// Gets a formatted failed assertion result, or throws.
    /// </summary>
    /// <typeparam name="T">The actual value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="format">The assertion failure format.</param>
    /// <param name="because">The "because" phrase.</param>
    /// <param name="arguments">The assertion failure arguments.</param>
    /// <returns>The assertion result.</returns>
    internal static AssertResult Failure<T>(this IAssertContext<T> context, string format, string? because, params object?[] arguments)
    {
        var formattedArguments = arguments.Select(a => FormatArgument(a)).ToArray();
        var result = string.Format(format, formattedArguments);
        because = string.IsNullOrWhiteSpace(because) ? string.Empty : $" because {because}";
        return context.Failure(result.Replace("{because}", because));
    }

    private static string Because(string? because)
        => string.IsNullOrWhiteSpace(because) ? string.Empty : $" because {because}";

    private static string FormatArgument(object? argument)
    {
        if (argument == null)
        {
            return "<null>";
        }

        if (argument is string text)
        {
            if (text.StartsWith('"') && text.EndsWith('"'))
            {
                return text;
            }

            return $"\"{text}\"";
        }

        if (argument is Exception e)
        {
            return string.IsNullOrEmpty(e.Message)
                ? $"<Exception: {e.GetType().Name}()>"
                : $"<Exception: {e.GetType().Name}(\"{e.Message}\")>";
        }

        return argument.ToString() !;
    }
}
