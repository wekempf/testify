namespace Testify;

/// <summary>
/// Assertion extensions for testing <see cref="Exception"/> results.
/// </summary>
public static class ExceptionAssertions
{
    private static readonly Regex LambdaPattern = new Regex(@"^\s*\(\s*\)\s*=>\s*");

    /// <summary>
    /// Asserts that the specified <see cref="Action"/> should throw the specified <see cref="Exception"/>.
    /// </summary>
    /// <typeparam name="TException">The type of the expected exception.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="additionalAssertion">An action to perform additional assertions.</param>
    /// <param name="because">The reason for the expected exception.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult Throws<TException>(this IAssertContext<Action> context, Func<TException, AssertResult>? additionalAssertion = default, string? because = default)
        where TException : Exception
    {
        Guard.Against.Null(context);

        try
        {
            context.ActualValue!.Invoke();
        }
        catch (TException ex)
        {
            if (additionalAssertion != null)
            {
                var result = additionalAssertion.Invoke(ex);
                if (result.IsFailure)
                {
                    return context.Failure(
                        "{0} was expected to throw {1}{{because}}, which it did but failed the additional assertion.\n{2}",
                        because,
                        LambdaPattern.Replace(context.ActualExpression, string.Empty),
                        typeof(TException),
                        IndentFailure(result));
                }
            }

            return context.Success;
        }
        catch (Exception ex)
        {
            return context.Failure(
                "{0} was expected to throw {1}{{because}} but threw {2} instead.",
                because,
                LambdaPattern.Replace(context.ActualExpression, string.Empty),
                typeof(TException),
                ex.GetType());
        }

        return context.Failure(
            "{0} was expected to throw {1}{{because}} but did not throw.",
            because,
            LambdaPattern.Replace(context.ActualExpression, string.Empty),
            typeof(TException));
    }

    /// <summary>
    /// Asserts the specified <see cref="Action"/> should not throw.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="because">The reason for the expected exception.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult DoesNotThrow(this IAssertContext<Action> context, string? because = default)
    {
        Guard.Against.Null(context);

        try
        {
            context.ActualValue!.Invoke();
        }
        catch (Exception ex)
        {
            return context.Failure(
                "{0} was expected to not throw{{because}}, but threw {1}.",
                because,
                context.ActualExpression,
                ex.GetType());
        }

        return context.Success;
    }
}
