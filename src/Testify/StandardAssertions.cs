namespace Testify;

/// <summary>
/// Provides standard assertions for most types.
/// </summary>
public static class StandardAssertions
{
    /// <summary>
    /// Asserts that the actual value should be equal to an expected value.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expectedValue"/> is different from the actual value by more than the <paramref name="delta"/>.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsEqualTo(this IAssertContext<double> context, double expectedValue, double delta, string? because = default)
    {
        Guard.Against.Null(context);

        if (double.IsNaN(context.ActualValue))
        {
            return context.Failure($"{context.ActualExpression} is NaN.");
        }

        if (double.IsNaN(expectedValue))
        {
            return context.Failure("The expected value is NaN");
        }

        if (double.IsNaN(delta))
        {
            return context.Failure("The delta is NaN");
        }

        var diff = Math.Abs(expectedValue - context.ActualValue);
        if (diff > delta)
        {
            return context.Failure(
                "Expected the difference between {0} and {1} to be less than {3}{{because}}, but the actual value {4} differs by {5}.",
                because,
                context.ActualExpression,
                expectedValue,
                delta,
                context.ActualValue,
                diff);
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts that the actual value should be equal to an expected value.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expectedValue"/> is different from the actual value by more than the <paramref name="delta"/>.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsEqualTo(this IAssertContext<float> context, float expectedValue, float delta, string? because = default)
    {
        Guard.Against.Null(context);

        if (double.IsNaN(context.ActualValue))
        {
            return context.Failure($"{context.ActualExpression} is NaN.");
        }

        if (double.IsNaN(expectedValue))
        {
            return context.Failure("The expected value is NaN");
        }

        if (double.IsNaN(delta))
        {
            return context.Failure("The delta is NaN");
        }

        var diff = Math.Abs(expectedValue - context.ActualValue);
        if (diff > delta)
        {
            return context.Failure(
                "Expected the difference between {0} and {1} to be less than {3}{{because}}, but the actual value {4} differs by {5}.",
                because,
                context.ActualExpression,
                expectedValue,
                delta,
                context.ActualValue,
                diff);
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts that the actual value should be equal to an expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="comparer">The comparer used to compare equality.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsEqualTo<T>(this IAssertContext<T> context, T expectedValue, IEqualityComparer<T>? comparer = default, string? because = default)
    {
        Guard.Against.Null(context);

        comparer ??= EqualityComparer<T>.Default;
        return comparer.Equals(context.ActualValue, expectedValue)
            ? context.Success
            : context.Failure(
                "Expected {0} to be {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedValue,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be equal to an expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsEqualTo<T>(this IAssertContext<T> context, object expectedValue, string? because = default)
    {
        Guard.Against.Null(context);

        return Equals(context.ActualValue, expectedValue)
            ? context.Success
            : context.Failure(
                "Expected {0} to be {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedValue,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should not be equal to an expected value.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expectedValue"/> is different from the actual value by more than the <paramref name="delta"/>.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotEqualTo(this IAssertContext<double> context, double expectedValue, double delta, string? because = default)
    {
        Guard.Against.Null(context);

        if (double.IsNaN(context.ActualValue))
        {
            return context.Failure($"{context.ActualExpression} is NaN.");
        }

        if (double.IsNaN(expectedValue))
        {
            return context.Failure("The expected value is NaN");
        }

        if (double.IsNaN(delta))
        {
            return context.Failure("The delta is NaN");
        }

        var diff = Math.Abs(expectedValue - context.ActualValue);
        if (diff <= delta)
        {
            return context.Failure(
                "Expected the difference between {0} and {1} to not be less than or equal to {3}{{because}}, but the actual value {4} differs by {5}.",
                because,
                context.ActualExpression,
                expectedValue,
                delta,
                context.ActualValue,
                diff);
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts that the actual value should not be equal to an expected value.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expectedValue"/> is different from the actual value by more than the <paramref name="delta"/>.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotEqualTo(this IAssertContext<float> context, float expectedValue, float delta, string? because = default)
    {
        Guard.Against.Null(context);

        if (double.IsNaN(context.ActualValue))
        {
            return context.Failure($"{context.ActualExpression} is NaN.");
        }

        if (double.IsNaN(expectedValue))
        {
            return context.Failure("The expected value is NaN");
        }

        if (double.IsNaN(delta))
        {
            return context.Failure("The delta is NaN");
        }

        var diff = Math.Abs(expectedValue - context.ActualValue);
        if (diff <= delta)
        {
            return context.Failure(
                "Expected the difference between {0} and {1} to not be less than or equal to {3}{{because}}, but the actual value {4} differs by {5}.",
                because,
                context.ActualExpression,
                expectedValue,
                delta,
                context.ActualValue,
                diff);
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts that the actual value should not be equal to an expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="comparer">The comparer used to compare equality.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotEqualTo<T>(this IAssertContext<T> context, T expectedValue, IEqualityComparer<T>? comparer = default, string? because = default)
    {
        Guard.Against.Null(context);

        comparer ??= EqualityComparer<T>.Default;
        return !comparer.Equals(context.ActualValue, expectedValue)
            ? context.Success
            : context.Failure(
                "Expected {0} to not be {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedValue,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should not be equal to an expected value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotEqualTo<T>(this IAssertContext<T> context, object expectedValue, string? because = default)
    {
        Guard.Against.Null(context);

        return !Equals(context.ActualValue, expectedValue)
            ? context.Success
            : context.Failure(
                "Expected {0} to not be {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedValue,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be greater than an expected minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedMinimum">The expected minimum value.</param>
    /// <param name="comparer">The comparer used to compare.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsGreaterThan<T>(this IAssertContext<T> context, T expectedMinimum, IComparer<T>? comparer = default, string? because = default)
        where T : IComparable
    {
        Guard.Against.Null(context);

        comparer ??= Comparer<T>.Default;
        return comparer.Compare(context.ActualValue, expectedMinimum) > 0
            ? context.Success
            : context.Failure(
                "Expected {0} to be greater than {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedMinimum,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be greater than or equal to an expected minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedMinimum">The expected minimum value.</param>
    /// <param name="comparer">The comparer used to compare.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsGreaterThanOrEqualTo<T>(this IAssertContext<T> context, T expectedMinimum, IComparer<T>? comparer = default, string? because = default)
        where T : IComparable
    {
        Guard.Against.Null(context);

        comparer ??= Comparer<T>.Default;
        return comparer.Compare(context.ActualValue, expectedMinimum) >= 0
            ? context.Success
            : context.Failure(
                "Expected {0} to be greater than or equal to {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedMinimum,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be less than an expected minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedMaximum">The expected minimum value.</param>
    /// <param name="comparer">The comparer used to compare.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsLessThan<T>(this IAssertContext<T> context, T expectedMaximum, IComparer<T>? comparer = default, string? because = default)
        where T : IComparable
    {
        Guard.Against.Null(context);

        comparer ??= Comparer<T>.Default;
        return comparer.Compare(context.ActualValue, expectedMaximum) < 0
            ? context.Success
            : context.Failure(
                "Expected {0} to be less than {1}{{because}}, but found {2}.",
                because,
                context.ActualExpression,
                expectedMaximum,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be less than or equal to an expected minimum value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedMaximum">The expected maximum value.</param>
    /// <param name="comparer">The comparer used to compare.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsLessThanOrEqualTo<T>(this IAssertContext<T> context, T expectedMaximum, IComparer<T>? comparer = default, string? because = default)
        where T : IComparable
    {
        Guard.Against.Null(context);

        comparer ??= Comparer<T>.Default;
        return comparer.Compare(context.ActualValue, expectedMaximum) <= 0
            ? context.Success
            : context.Failure(
                "Expected {0} to be less than or equal to {1}{{because}}, bout found {2}.",
                because,
                context.ActualExpression,
                expectedMaximum,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts that the actual value should be in the expected range.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedMinimum">The expected minimum value.</param>
    /// <param name="expectedMaximum">The expected maximum value.</param>
    /// <param name="comparer">The comparer used to compare.</param>
    /// <param name="minimumInclusive">Whether or not <paramref name="expectedMinimum"/> is inclusive to the range.</param>
    /// <param name="maximumInclusive">Whether or not <paramref name="expectedMaximum"/> is inclusive to the range.</param>
    /// <param name="because">The reason for the expected range.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsInRange<T>(
        this IAssertContext<T> context,
        T expectedMinimum,
        T expectedMaximum,
        IComparer<T>? comparer = default,
        bool minimumInclusive = true,
        bool maximumInclusive = true,
        string? because = default)
    {
        Guard.Against.Null(context);

        comparer = Comparer<T>.Default;
        var minimumComparison = comparer.Compare(context.ActualValue, expectedMinimum);
        var maximumComparison = comparer.Compare(context.ActualValue, expectedMaximum);
        return (minimumComparison > 0 || (minimumInclusive && minimumComparison == 0)) &&
            (maximumComparison < 0 || (maximumInclusive && maximumComparison == 0))
            ? context.Success
            : context.Failure(
                "Expected {0} to be in the range {1}{2}, {3}{4}{{because}}, but found {5}.",
                because,
                context.ActualExpression,
                minimumInclusive ? "[" : "(",
                expectedMinimum,
                expectedMaximum,
                maximumInclusive ? "]" : ")",
                context.ActualValue);
    }

    /// <summary>
    /// Asserts the actual value should be false.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsFalse(this IAssertContext<bool> context, string? because = default)
    {
        Guard.Against.Null(context);

        return context.ActualValue == false
            ? context.Success
            : context.Failure(
                "Expected {0} to be false{{because}}.",
                because,
                context.ActualExpression);
    }

    /// <summary>
    /// Asserts the actual value should be false.
    /// </summary>
    /// <param name="context">The assertion context.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsTrue(this IAssertContext<bool> context, string? because = default)
    {
        Guard.Against.Null(context);

        return context.ActualValue == true
            ? context.Success
            : context.Failure(
                "Expected {0} to be true{{because}}.",
                because,
                context.ActualExpression);
    }

    /// <summary>
    /// Asserts the actual value is an instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedType">The type the actual value is expected to be an instance of.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsInstanceOfType<T>(this IAssertContext<T> context, Type expectedType, string? because = default)
    {
        Guard.Against.Null(context);
        Guard.Against.Null(expectedType);

        if (context.ActualValue == null)
        {
            return context.Failure(
                "Expected {0} to be instance of type {1}{{because}}, but the value is null.",
                because,
                context.ActualExpression,
                expectedType);
        }

        if (!expectedType.IsInstanceOfType(context.ActualValue))
        {
            return context.Failure(
                "Expected {0} to be instance of type {1}{{because}}, but has a type of {2}.",
                because,
                context.ActualExpression,
                expectedType,
                context.ActualValue.GetType());
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts the actual value is not an instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expectedType">The type the actual value is expected to be an instance of.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotInstanceOfType<T>(this IAssertContext<T> context, Type expectedType, string? because = default)
    {
        Guard.Against.Null(context);
        Guard.Against.Null(expectedType);

        if (context.ActualValue == null || !expectedType.IsInstanceOfType(context.ActualValue))
        {
            return context.Success;
        }

        return context.Failure(
            "Expected {0} to not be an instance of type {1}{{because}}, but it is an instance of the type.",
            because,
            context.ActualExpression,
            expectedType);
    }

    /// <summary>
    /// Asserts the actual value should be null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNull<T>(this IAssertContext<T> context, string? because = default)
        where T : class
    {
        Guard.Against.Null(context);

        return context.ActualValue != null
            ? context.Success
            : context.Failure(
                "Expected {0} to be null{{because}}, but was {1}.",
                because,
                context.ActualExpression,
                context.ActualValue);
    }

    /// <summary>
    /// Asserts the actual value should not be null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotNull<T>(this IAssertContext<T> context, string? because = default)
        where T : class
    {
        Guard.Against.Null(context);

        return context.ActualValue != null
            ? context.Success
            : context.Failure(
                "Expected '{0}' to be not null{{because}}, but was null.",
                because,
                context.ActualExpression);
    }

    /// <summary>
    /// Asserts the actual value should be the same instance as the expected reference.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expected">The expected reference.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsSameAs<T>(this IAssertContext<T> context, object expected, string? because = default)
        where T : class
    {
        Guard.Against.Null(context);
        Guard.Against.Null(expected);

        if (!ReferenceEquals(expected, context.ActualValue))
        {
            return context.Failure(
                "Expected {0} to be the same as the expected referece{{because}}, but it was not.",
                because,
                context.ActualExpression);
        }

        return context.Success;
    }

    /// <summary>
    /// Asserts the actual value should not be the same instance as the expected reference.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <param name="context">The assertion context.</param>
    /// <param name="expected">The expected reference.</param>
    /// <param name="because">The reason for the expected value.</param>
    /// <returns>The <see cref="AssertResult"/>.</returns>
    /// <exception cref="Exception">Thrown for immediate assertion failures (Assert clauses). The exact exception type will depend on the testing framework.</exception>
    public static AssertResult IsNotSameAs<T>(this IAssertContext<T> context, object expected, string? because = default)
        where T : class
    {
        Guard.Against.Null(context);
        Guard.Against.Null(expected);

        if (ReferenceEquals(expected, context.ActualValue))
        {
            return context.Failure(
                "Expected {0} to not be the same as the expected referece{{because}}, but it was.",
                because,
                context.ActualExpression);
        }

        return context.Success;
    }
}
