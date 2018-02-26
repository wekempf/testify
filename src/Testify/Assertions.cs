using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Testify.Properties;
using static Testify.FrameworkMessages;

namespace Testify
{
    /// <summary>
    /// Provides methods for starting fluent assertions.
    /// </summary>
    public static class Assertions
    {
        /// <summary>
        /// Begins a fluent assertion by providing the actual value.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <returns>An <see cref="ActualValue{T}"/> instance that can be used to declare
        /// fluent assertions.</returns>
        public static ActualValue<T> Assert<T>(T actualValue) =>
            new ActualValue<T>(actualValue);

        /// <summary>
        /// Begins a fluent assertion by providing an <see cref="Action{T}"/> delegate.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>An <see cref="ActualValue{T}"/> instance that can be used to declare
        /// fluent assertions.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
        public static ActualValue<Action> Assert(Action action)
        {
            Argument.NotNull(action, nameof(action));

            return new ActualValue<Action>(action);
        }

        /// <summary>
        /// Declares a compound assertion.
        /// </summary>
        /// <param name="message">The message to display if any of the assertions fail.</param>
        /// <param name="assertions">A list of actions to be invoked to make assertions.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="message"/> or <paramref name="assertions"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> is empty.</exception>
        public static void AssertAll(string message, params Action[] assertions)
        {
            Argument.NotNullOrEmpty(message, nameof(message));
            Argument.NotNull(assertions, nameof(assertions));

            AssertAll(message, (IEnumerable<Action>)assertions);
        }

        /// <summary>
        /// Declares a compound assertion.
        /// </summary>
        /// <param name="message">The message to display if any of the assertions fail.</param>
        /// <param name="assertions">A list of actions to be invoked to make assertions.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="message"/> or <paramref name="assertions"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> is empty.</exception>
        public static void AssertAll(string message, IEnumerable<Action> assertions)
        {
            Argument.NotNullOrEmpty(message, nameof(message));
            Argument.NotNull(assertions, nameof(assertions));

            var errors = new Lazy<List<Exception>>(false);
            foreach (var action in assertions)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    errors.Value.Add(e);
                }
            }

            if (errors.IsValueCreated)
            {
                throw new AssertionException(message, errors.Value.ToArray());
            }
        }

        /// <summary>
        /// Fails with an <see cref="AssertionException"/> without checking any conditions. Displays
        /// a message.
        /// </summary>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void Fail(string message) => throw new AssertionException(message);

        /// <summary>
        /// Fails with an <see cref="AssertionException"/> without checking any conditions. Displays
        /// a message, and applies the specified formatting to it.
        /// </summary>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void Fail(string message, params object[] parameters)
        {
            var completeMessage = Assertions.CreateCompleteMessage(message, parameters);
            throw new AssertionException(completeMessage);
        }

        /// <summary>
        /// In a string, replaces null characters ("\0") with "\\0".
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The string with null characters replaced.</returns>
        public static string ReplaceNullChars(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var list = new List<int>();
            for (var index = 0; index < input.Length; ++index)
            {
                if (input[index] == 0)
                {
                    list.Add(index);
                }
            }

            if (!list.Any())
            {
                return input;
            }

            var stringBuilder = new StringBuilder(input.Length + list.Count);
            var startIndex = 0;
            foreach (var num in list)
            {
                stringBuilder.Append(input, startIndex, num - startIndex);
                stringBuilder.Append("\\0");
                startIndex = num + 1;
            }

            stringBuilder.Append(input.Substring(startIndex));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Throws the specified assertion name.
        /// </summary>
        /// <param name="assertionName">Name of the assertion.</param>
        /// <param name="assertionMessage">The assertion message.</param>
        /// <param name="userMessage">The user message.</param>
        /// <param name="userArgs">The user arguments used to format the user message.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Throw(string assertionName, string assertionMessage, string userMessage, params object[] userArgs)
        {
            Argument.NotNullOrEmpty(assertionName, nameof(assertionName));

            var message = AssertionFailed(assertionName, assertionMessage, userMessage, userArgs);
            throw new AssertionException(message);
        }

        /// <summary>
        /// Throws the specified assertion name.
        /// </summary>
        /// <param name="assertionName">Name of the assertion.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="assertionMessage">The assertion message.</param>
        /// <param name="userMessage">The user message.</param>
        /// <param name="userArgs">The user arguments used to format the user message.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Throw(
            string assertionName,
            Exception innerException,
            string assertionMessage,
            string userMessage,
            params object[] userArgs)
        {
            Argument.NotNullOrEmpty(assertionName, nameof(assertionName));

            var message = AssertionFailed(assertionName, assertionMessage, userMessage, userArgs);
            throw new AssertionException(message, innerException);
        }

        /// <summary>
        /// Throws the specified assertion name.
        /// </summary>
        /// <param name="assertionName">Name of the assertion.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <param name="assertionMessage">The assertion message.</param>
        /// <param name="userMessage">The user message.</param>
        /// <param name="userArgs">The user arguments used to format the user message.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Throw(
            string assertionName,
            IEnumerable<Exception> innerExceptions,
            string assertionMessage,
            string userMessage,
            params object[] userArgs)
        {
            Argument.NotNullOrEmpty(assertionName, nameof(assertionName));

            var message = AssertionFailed(assertionName, assertionMessage, userMessage, userArgs);
            throw new AssertionException(message, innerExceptions.ToArray());
        }

        /// <summary>
        /// Creates the complete message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The formatted message.</returns>
        internal static string CreateCompleteMessage(string message, object[] parameters)
        {
            var str = string.Empty;
            if (!string.IsNullOrEmpty(message))
            {
                str = parameters != null
                    ? string.Format(CultureInfo.CurrentCulture, ReplaceNulls(message), parameters)
                    : ReplaceNulls(message);
            }

            return str;
        }

        /// <summary>
        /// Replaces the nulls.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Text formatted without nulls.</returns>
        internal static string ReplaceNulls(object input)
        {
            if (input == null)
            {
                return Resources.Common_NullInMessages;
            }

            var result = input.ToString();
            if (result == null)
            {
                return Resources.Common_ObjectString;
            }

            return Assertions.ReplaceNullChars(result);
        }
    }
}