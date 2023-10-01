namespace Testify;
using System;

/// <summary>
/// Provides fluent assertions for exceptions.
/// </summary>
public static class ExceptionAssertions
{
    /// <summary>
    /// Asserts that the action, when invoked, should throw an <see cref="Exception"/>.
    /// </summary>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    /// <returns>An <see cref="IAdditionalThrowAssertions{T}"/> instance which
    ///     can be used to make additional assertions about the exception
    ///     thrown.</returns>
    public static IAdditionalThrowAssertions<Exception> ShouldThrow(this ActualValue<Action> actual, string? because = null)
        => ShouldThrow<Exception>(actual, because);

    /// <summary>
    /// Asserts that the action, when called, should throw the specified
    /// exception type.
    /// </summary>
    /// <typeparam name="T">The exception type expected to be thrown.
    ///     </typeparam>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    /// <returns>An <see cref="IAdditionalThrowAssertions{T}"/> instance which
    ///     can be used to make additional assertions about the exception
    ///     thrown.</returns>
    public static IAdditionalThrowAssertions<T> ShouldThrow<T>(this ActualValue<Action> actual, string? because = null)
        where T : Exception
    {
        Guard.Against.Null(actual);
        Guard.Against.Null(actual.Value);

        try
        {
            actual.Value.Invoke();
        }
        catch (T e)
        {
            return new AdditionalThrowAssertions<T>(e);
        }
        catch (Exception e)
        {
            Fail(
                $"Expected {actual:e} to throw an exception of type {Format(typeof(T))}{{because}}, but it threw an exception of type {Format(e.GetType())} instead.",
                because);
            return new AdditionalThrowAssertions<T>();
        }

        Fail(
            $"Expected {actual:e} to throw an exception of type {Format(typeof(T))}{{because}}, but it did not throw.",
            because);
        return new AdditionalThrowAssertions<T>();
    }

    /// <summary>
    /// Asserts that the action, when invoked, should not throw any exceptions.
    /// </summary>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    /// <remarks>
    /// This assertion isn't strictly needed as code that throws will fail a
    /// test. However, using
    /// <see cref="ShouldNotThrow(ActualValue{Action}, string?)"/> does a better
    /// job describing the intent of the test code, as well as provides a better
    /// test failure message.
    /// </remarks>
    public static void ShouldNotThrow(this ActualValue<Action> actual, string? because)
    {
        Guard.Against.Null(actual);
        Guard.Against.Null(actual.Value);

        try
        {
            actual.Value.Invoke();
        }
        catch (Exception e)
        {
            Ignore(e);
            Fail($"Expected {actual:e} to not throw an exception{{because}}, but it did.", because);
        }

        static void Ignore(Exception _) { }
    }

    /// <summary>
    /// Asserts the <see cref="Action"/>, when invoked, should not throw the
    /// specified exception type.
    /// </summary>
    /// <typeparam name="T">The exception type not expected to be thrown.
    ///     </typeparam>
    /// <param name="actual">The <see cref="ActualValue{T}"/> instance that
    ///     represents the actual value being asserted on.</param>
    /// <param name="because">The user supplied "because phrase" describing why
    ///     the assertion is being made.</param>
    /// <remarks>
    /// This assertion isn't strictly needed as code that throws will fail a
    /// test. However, using
    /// <see cref="ShouldNotThrow(ActualValue{Action}, string?)"/> does a better
    /// job describing the intent of the test code, as well as provides a better
    /// test failure message.
    /// </remarks>
    public static void ShouldNotThrow<T>(this ActualValue<Action> actual, string? because)
        where T : Exception
    {
        Guard.Against.Null(actual);
        Guard.Against.Null(actual.Value);

        try
        {
            actual.Value.Invoke();
        }
        catch (T e)
        {
            Ignore(e);
            Fail($"Expected {actual:e} to not throw an exception of type {Format(typeof(T))}{{because}}, but it did.", because);
        }

        static void Ignore(Exception _) { }
    }

    /// <summary>
    /// Result from throw assertions that provides fluent method for making
    /// additional assertions about the exception thrown.
    /// </summary>
    /// <typeparam name="T">The exception type.</typeparam>
    public interface IAdditionalThrowAssertions<T>
        where T : Exception
    {
        /// <summary>
        /// Called to provide additional assertions about the exception that was
        /// thrown.
        /// </summary>
        /// <param name="action">The action called to make additional assertions
        ///     about the exception that was thrown.</param>
        void AndShouldSatisfy(Action<ActualValue<T>> action);
    }

    private class AdditionalThrowAssertions<T> : IAdditionalThrowAssertions<T>
        where T : Exception
    {
        private readonly bool _shouldInvoke;
        private readonly T? _value;

        public AdditionalThrowAssertions()
        {
            _shouldInvoke = false;
        }

        public AdditionalThrowAssertions(T value)
        {
            Guard.Against.Null(value);
            _value = value;
            _shouldInvoke = true;
        }

        public void AndShouldSatisfy(Action<ActualValue<T>> action)
        {
            if (_shouldInvoke)
            {
                Assert(_value, "exception").ShouldSatisfy(action);
            }
        }
    }
}
