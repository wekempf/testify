using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Testify.Properties;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Provides methods for obtaining formatted framework messages.
    /// </summary>
    internal static class FrameworkMessages
    {
        /// <summary>
        /// Additionals the assertions failed.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <returns>The formatted message.</returns>
        internal static string AdditionalAssertionsFailed(Type expectedExceptionType) =>
            Format(
                Resources.FrameworkMessage_AdditionalAssertionsFailed,
                expectedExceptionType).Trim();

        /// <summary>
        /// Formats a framework message for assertion failures.
        /// </summary>
        /// <param name="assertionName">Name of the assertion.</param>
        /// <param name="assertionMessage">Assertion message.</param>
        /// <param name="userMessage">User message or format string.</param>
        /// <param name="userArgs">User arguments.</param>
        /// <returns>The formatted text.</returns>
        internal static string AssertionFailed(
            string assertionName,
            string assertionMessage,
            string userMessage,
            params object[] userArgs) =>
            Format(
                Resources.FrameworkMessage_AssertionFailed,
                assertionName,
                assertionMessage,
                (userArgs != null && userArgs.Any()) ? Format(ReplaceNullChars(userMessage), userArgs) : ReplaceNullChars(userMessage));

        /// <summary>
        /// Collections the equal reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>The formatted message.</returns>
        internal static string CollectionEqualReason(string reason) =>
            Format(
                Resources.FrameworkMessage_CollectionEqualReason,
                reason);

        /// <summary>
        /// Collections the equal reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>The formatted message.</returns>
        internal static string CollectionNotEqualReason(string reason) =>
            Format(
                Resources.FrameworkMessage_CollectionNotEqualReason,
                reason);

        /// <summary>
        /// Determines whether [contains fail MSG] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <returns>The formatted message.</returns>
        internal static string ContainsFailMsg(string value, string substring) =>
            Format(
                Resources.FrameworkMessage_ContainsFailMsg,
                value,
                substring);

        /// <summary>
        /// Dids the not throw.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The formatted message.</returns>
        internal static string DidNotThrow(Type type) => Format(Resources.FrameworkMessage_DidNotThrow, type);

        /// <summary>
        /// Doeses the not match fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The formatted message.</returns>
        internal static string DoesNotMatchFailMsg(string value, Regex pattern) =>
            Format(
                Resources.FrameworkMessage_DoesNotMatchFailMsg,
                value,
                pattern);

        /// <summary>
        /// Formats a message for duplicate item failures.
        /// </summary>
        /// <param name="duplicateValue">The duplicate value.</param>
        /// <returns>The formatted message.</returns>
        internal static string DuplicateFound(object duplicateValue) =>
            Format(
                Resources.FrameworkMessage_DuplicateFound,
                ReplaceNulls(duplicateValue));

        /// <summary>
        /// Elementses at index do not match.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <returns>The formatted message.</returns>
        internal static string ElementsAtIndexDoNotMatch(int index, object expected, object actual) =>
            Format(
                Resources.FrameworkMessage_ElementsAtIndexDoNotMatch,
                index,
                expected,
                actual);

        /// <summary>
        /// Endses the with fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <returns>The formatted message.</returns>
        internal static string EndsWithFailMsg(string value, string substring) =>
            Format(
                Resources.FrameworkMessage_EndsWithFailMsg,
                value,
                substring);

        /// <summary>
        /// Formats a framework message for equality failures with a delta.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The formatted text.</returns>
        internal static string IsEqualToDeltaFailMsg<T>(T expected, T actual, T delta) =>
            Format(
                Resources.FrameworkMessage_IsEqualToDeltaFailMsg,
                delta,
                expected,
                actual);

        /// <summary>
        /// Formats a framework message for equality failures where the types differ.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsEqualToDifferentTypesFailMsg(string expected, string expectedType, string actual, string actualType) =>
            Format(
                Resources.FrameworkMessage_IsEqualToFailTypesMsg,
                expected,
                expectedType,
                actual,
                actualType);

        /// <summary>
        /// Formats a framework message for equality failures.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsEqualToFailMsg(string expected, string actual) =>
            Format(
                Resources.FrameworkMessage_IsEqualToFailMsg,
                expected,
                actual);

        /// <summary>
        /// Determines whether [is instance of type fail MSG] [the specified message].
        /// </summary>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualValue">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsInstanceOfTypeFailMsg(Type expectedType, object actualValue) =>
            Format(
                Resources.FrameworkMessage_IsInstanceOfTypeFailMsg,
                expectedType,
                actualValue?.GetType()?.FullName ?? Resources.Common_NullInMessages);

        /// <summary>
        /// Formats a framework message for inequality failures.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotEqualToDeltaFailMsg<T>(T expected, T actual, T delta) =>
            Format(
                Resources.FrameworkMessage_IsNotEqualToDeltaFailMsg,
                delta,
                expected,
                actual);

        /// <summary>
        /// Formats a framework message for inequality failures.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotEqualToFailMsg(object expected, object actual) =>
            Format(
                Resources.FrameworkMessage_IsNotEqualToFailMsg,
                ReplaceNulls(expected),
                ReplaceNulls(actual));

        /// <summary>
        /// Determines whether [is instance of type fail MSG] [the specified message].
        /// </summary>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualValue">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotInstanceOfTypeFailMsg(Type expectedType, object actualValue) =>
            Format(
                Resources.FrameworkMessage_IsNotInstanceOfTypeFailMsg,
                expectedType,
                actualValue?.GetType()?.ToString() ?? Resources.Common_NullInMessages);

        /// <summary>
        /// Doeses the not match fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The formatted message.</returns>
        internal static string MatchesFailMsg(string value, Regex pattern) =>
            Format(
                Resources.FrameworkMessage_MatchesFailMsg,
                value,
                pattern);

        /// <summary>
        /// Nulls the parameter to assert.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The formatted message.</returns>
        internal static string NullParameterToAssert(string parameterName) =>
            Format(
                Resources.FrameworkMessage_NullParameterToAssert,
                parameterName);

        /// <summary>
        /// Endses the with fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <returns>The formatted message.</returns>
        internal static string StartsWithFailMsg(string value, string substring) =>
            Format(
                Resources.FrameworkMessage_StartsWithFailMsg,
                value,
                substring);

        /// <summary>
        /// Unexpecteds the exception.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <param name="actualExceptionType">Actual type of the exception.</param>
        /// <returns>The formatted message.</returns>
        internal static string UnexpectedException(Type expectedExceptionType, Type actualExceptionType) =>
            Format(
                Resources.FrameworkMessage_UnexpectedException,
                expectedExceptionType,
                actualExceptionType);

        /// <summary>
        /// Formats a message for unexpected type failures.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns>The formatted message.</returns>
        internal static string UnexpectedTypeAt(int index, Type expectedType, Type actualType) =>
            Format(
                Resources.FrameworkMessage_UnexpectedTypeAt,
                index,
                expectedType,
                actualType);

        /// <summary>
        /// Formats a message for Task not completing failures.
        /// </summary>
        /// <returns>The message.</returns>
        internal static string TaskDidNotComplete() =>
            Resources.FrameworkMessage_TaskDidNotComplete;

        private static string Format(string format, params object[] args) =>
            string.Format(CultureInfo.CurrentCulture, format, args).Trim();
    }
}