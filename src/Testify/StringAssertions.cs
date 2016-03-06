using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Testify
{
    /// <summary>
    /// Provides assertion methods associated with strings in unit tests.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StringAssertions
    {
        /// <summary>
        /// Verifies that the string contains the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it. This method is case sensitive.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to occur within <paramref name="value"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not contain <paramref name="substring"/>.</exception>
        public static void Contains(this ActualValue<string> value, string substring)
        {
            Argument.NotNull(value, nameof(value));

            value.Contains(substring, (string)null, (object[])null);
        }

        /// <summary>
        /// Verifies that the string contains the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it. This method is case sensitive.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to occur within <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not contain <paramref name="substring"/>.</exception>
        public static void Contains(this ActualValue<string> value, string substring, string message)
        {
            Argument.NotNull(value, nameof(value));

            value.Contains(substring, message, (object[])null);
        }

        /// <summary>
        /// Verifies that the string contains the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it. This method is case sensitive.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to occur within <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not contain <paramref name="substring"/>.</exception>
        public static void Contains(this ActualValue<string> value, string substring, string message, params object[] parameters)
        {
            Argument.NotNull(value, nameof(value));

            if (value.Value.IndexOf(substring, StringComparison.Ordinal) >= 0)
            {
                return;
            }

            message = FrameworkMessages.ContainsFailMsg(value.Value, substring, message);
            Assertions.HandleFail("Contains", message, parameters);
        }

        /// <summary>
        /// Verifies that the specified string does not match the regular expression. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is not expected to match.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> matches <paramref name="pattern"/>.</exception>
        public static void DoesNotMatch(this ActualValue<string> value, Regex pattern)
        {
            Argument.NotNull(value, nameof(value));

            value.DoesNotMatch(pattern, null, null);
        }

        /// <summary>
        /// Verifies that the specified string does not match the regular expression. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is not expected to match.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> matches <paramref name="pattern"/>.</exception>
        public static void DoesNotMatch(this ActualValue<string> value, Regex pattern, string message)
        {
            Argument.NotNull(value, nameof(value));

            value.DoesNotMatch(pattern, message, null);
        }

        /// <summary>
        /// Verifies that the specified string does not match the regular expression. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is not expected to match.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> matches <paramref name="pattern"/>.</exception>
        public static void DoesNotMatch(this ActualValue<string> value, Regex pattern, string message, params object[] parameters)
        {
            Argument.NotNull(value, nameof(value));

            if (!pattern.IsMatch(value.Value))
            {
                return;
            }

            message = FrameworkMessages.DoesNotMatchFailMsg(value.Value, pattern.ToString(), message);
            Assertions.HandleFail("DoesNotMatch", message, parameters);
        }

        /// <summary>
        /// Verifies that the specified string ends with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not end with <paramref name="substring"/>.</exception>
        public static void EndsWith(this ActualValue<string> value, string substring)
        {
            Argument.NotNull(value, nameof(value));

            value.EndsWith(substring, null, null);
        }

        /// <summary>
        /// Verifies that the specified string ends with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not end with <paramref name="substring"/>.</exception>
        public static void EndsWith(this ActualValue<string> value, string substring, string message)
        {
            Argument.NotNull(value, nameof(value));

            value.EndsWith(substring, message, null);
        }

        /// <summary>
        /// Verifies that the specified string ends with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not end with <paramref name="substring"/>.</exception>
        public static void EndsWith(this ActualValue<string> value, string substring, string message, params object[] parameters)
        {
            Argument.NotNull(value, nameof(value));

            if (value.Value.EndsWith(substring, StringComparison.Ordinal))
            {
                return;
            }

            message = FrameworkMessages.EndsWithFailMsg(value.Value, substring, message);
            Assertions.HandleFail("EndsWith", message, parameters);
        }

        /// <summary>
        /// Verifies that the specified string matches the regular expression. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is expected to match.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not match <paramref name="pattern"/>.</exception>
        public static void Matches(this ActualValue<string> value, Regex pattern)
        {
            Argument.NotNull(value, nameof(value));

            value.Matches(pattern, null, null);
        }

        /// <summary>
        /// Verifies that the specified string matches the regular expression. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is expected to match.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not match <paramref name="pattern"/>.</exception>
        public static void Matches(this ActualValue<string> value, Regex pattern, string message)
        {
            Argument.NotNull(value, nameof(value));

            value.Matches(pattern, message, null);
        }

        /// <summary>
        /// Verifies that the specified string matches the regular expression. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="pattern">The regular expression that <paramref name="value"/> is expected to match.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not match <paramref name="pattern"/>.</exception>
        public static void Matches(this ActualValue<string> value, Regex pattern, string message, params object[] parameters)
        {
            Argument.NotNull(value, nameof(value));

            if (pattern.IsMatch(value.Value))
            {
                return;
            }

            message = FrameworkMessages.MatchesFailMsg(value.Value, pattern.ToString(), message);
            Assertions.HandleFail("Matches", message, parameters);
        }

        /// <summary>
        /// Verifies that the specified string starts with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not start with <paramref name="substring"/>.</exception>
        public static void StartsWith(this ActualValue<string> value, string substring)
        {
            Argument.NotNull(value, nameof(value));

            value.StartsWith(substring, null, null);
        }

        /// <summary>
        /// Verifies that the specified string starts with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not start with <paramref name="substring"/>.</exception>
        public static void StartsWith(this ActualValue<string> value, string substring, string message)
        {
            Argument.NotNull(value, nameof(value));

            value.StartsWith(substring, message, null);
        }

        /// <summary>
        /// Verifies that the specified string starts with the specified substring. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="substring">The string expected to be a suffix of <paramref name="value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="AssertionException"><paramref name="value"/> does not start with <paramref name="substring"/>.</exception>
        public static void StartsWith(this ActualValue<string> value, string substring, string message, params object[] parameters)
        {
            Argument.NotNull(value, nameof(value));

            if (value.Value.StartsWith(substring, StringComparison.Ordinal))
            {
                return;
            }

            message = FrameworkMessages.StartsWithFailMsg(value.Value, substring, message);
            Assertions.HandleFail("StartsWith", message, parameters);
        }
    }
}