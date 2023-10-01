namespace Testify;

/// <summary>
/// Provides fluent assertions for <see cref="bool"/> values.
/// </summary>
public static class BooleanAssertions
{
    /// <summary>
    /// Asserts that the <paramref name="actual"/> value should be
    /// <see langword="true"/>.
    /// </summary>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    public static void ShouldBeTrue(this ActualValue<bool> actual, string? because = null)
    {
        Guard.Against.Null(actual);

        if (!actual.Value)
        {
            Fail($"Expected {actual:e} to be true{{because}}, but found false.", because);
        }
    }

    /// <summary>
    /// Asserts that the <paramref name="actual"/> value should be
    /// <see langword="false"/>.
    /// </summary>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    public static void ShouldBeFalse(this ActualValue<bool> actual, string? because = null)
    {
        Guard.Against.Null(actual);

        if (actual.Value)
        {
            Fail($"Expected {actual:e} to be false{{because}}, but found true.", because);
        }
    }
}
