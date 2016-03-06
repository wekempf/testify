using System;

namespace Testify
{
    /// <summary>
    /// Provides assertions for exceptions.
    /// </summary>
    public static class ExceptionAssertions
    {
        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            action.Throws<T>(null, null, null);
        }

        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <param name="additionalAssertion">The additional assertion.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action, Action<T> additionalAssertion)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            action.Throws<T>(additionalAssertion, null, null);
        }

        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action, string message)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            action.Throws<T>(null, message, null);
        }

        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <param name="additionalAssertion">The additional assertion.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action, Action<T> additionalAssertion, string message)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            action.Throws<T>(additionalAssertion, message, null);
        }

        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action, string message, params object[] parameters)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            action.Throws<T>(null, message, parameters);
        }

        /// <summary>
        /// Verifies that the specified action throws a specific exception type.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The exception.</param>
        /// <param name="additionalAssertion">The additional assertion.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or it's value is <see langword="null"/>.</exception>
        public static void Throws<T>(this ActualValue<Action> action, Action<T> additionalAssertion, string message, params object[] parameters)
            where T : Exception
        {
            Argument.NotNull(action, nameof(action));
            Argument.NotNull(action.Value, nameof(action));

            try
            {
                action.Value();
            }
            catch (T e)
            {
                try
                {
                    additionalAssertion?.Invoke(e);
                }
                catch (Exception additionalException)
                {
                    var additionalAssertionsFailedMessage = FrameworkMessages.AdditionalAssertionsFailed(typeof(T), message);
                    Assertions.HandleFail("Throws", additionalException, additionalAssertionsFailedMessage, parameters);
                }

                return;
            }
            catch (Exception e)
            {
                var unexpectedExceptionMessage = FrameworkMessages.UnexpectedException(typeof(T), e.GetType(), message);
                Assertions.HandleFail("Throws", unexpectedExceptionMessage, parameters);
            }

            var didNotThrowMessage = FrameworkMessages.DidNotThrow(typeof(T), message);
            Assertions.HandleFail("Throws", didNotThrowMessage, parameters);
        }
    }
}