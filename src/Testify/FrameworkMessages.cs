using System;
using System.Globalization;
using Testify.Properties;

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
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string AdditionalAssertionsFailed(Type expectedExceptionType, string message) =>
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.FrameworkMessage_AdditionalAssertionsFailed,
                        expectedExceptionType,
                        message).Trim();

        /// <summary>
        /// Formats a framework message for assertion failures.
        /// </summary>
        /// <param name="assertionName">Name of the assertion.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted text.</returns>
        internal static string AssertionFailed(string assertionName, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_AssertionFailed,
                assertionName,
                message).Trim();

        /// <summary>
        /// Collections the equal reason.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="reason">The reason.</param>
        /// <returns>The formatted message.</returns>
        internal static string CollectionEqualReason(string message, string reason) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_CollectionEqualReason,
                reason,
                message);

        /// <summary>
        /// Collections the equal reason.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="reason">The reason.</param>
        /// <returns>The formatted message.</returns>
        internal static string CollectionNotEqualReason(string message, string reason) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_CollectionNotEqualReason,
                reason,
                message);

        /// <summary>
        /// Determines whether [contains fail MSG] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string ContainsFailMsg(string value, string substring, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_ContainsFailMsg,
                value,
                substring,
                message).Trim();

        /// <summary>
        /// Dids the not throw.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string DidNotThrow(Type type, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_DidNotThrow,
                type,
                message).Trim();

        /// <summary>
        /// Doeses the not match fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string DoesNotMatchFailMsg(string value, string pattern, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_DoesNotMatchFailMsg,
                value,
                pattern,
                message).Trim();

        /// <summary>
        /// Duplicates the found.
        /// </summary>
        /// <param name="duplicateValue">The duplicate value.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string DuplicateFound(object duplicateValue, string message) =>
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.FrameworkMessage_DuplicateFound,
                        Assertions.ReplaceNulls(duplicateValue),
                        message);

        /// <summary>
        /// Elementses at index do not match.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <returns>The formatted message.</returns>
        internal static string ElementsAtIndexDoNotMatch(int index, object expected, object actual) =>
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.FrameworkMessage_ElementsAtIndexDoNotMatch,
                        index,
                        expected,
                        actual);

        /// <summary>
        /// Endses the with fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string EndsWithFailMsg(string value, string substring, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_EndsWithFailMsg,
                value,
                substring,
                message).Trim();

        /// <summary>
        /// Formats a framework message for equality failures with a delta.
        /// </summary>
        /// <param name="message">The user message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The formatted text.</returns>
        internal static string IsEqualToDeltaFailMsg(string message, string expected, string actual, string delta) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsEqualToDeltaFailMsg,
                delta,
                expected,
                actual,
                message).Trim();

        /// <summary>
        /// Formats a framework message for equality failures where the types differ.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsEqualToDifferentTypesFailMsg(string message, string expected, string expectedType, string actual, string actualType) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsEqualToFailTypesMsg,
                expected,
                expectedType,
                actual,
                actualType,
                message).Trim();

        /// <summary>
        /// Formats a framework message for equality failures.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsEqualToFailMsg(string message, string expected, string actual) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsEqualToFailMsg,
                expected,
                actual,
                message).Trim();

        /// <summary>
        /// Determines whether [is instance of type fail MSG] [the specified message].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsInstanceOfTypeFailMsg(string message, string expectedType, string actualType) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsInstanceOfTypeFailMsg,
                expectedType,
                actualType,
                message).Trim();

        /// <summary>
        /// Formats a framework message for inequality failures.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotEqualToDeltaFailMsg(string message, string expected, string actual, string delta) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsNotEqualToDeltaFailMsg,
                delta,
                expected,
                actual,
                message).Trim();

        /// <summary>
        /// Formats a framework message for inequality failures.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotEqualToFailMsg(string message, string expected, string actual) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsNotEqualToFailMsg,
                expected,
                actual,
                message).Trim();

        /// <summary>
        /// Determines whether [is instance of type fail MSG] [the specified message].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns>The formatted message.</returns>
        internal static string IsNotInstanceOfTypeFailMsg(string message, string expectedType, string actualType) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_IsNotInstanceOfTypeFailMsg,
                expectedType,
                actualType,
                message).Trim();

        /// <summary>
        /// Doeses the not match fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string MatchesFailMsg(string value, string pattern, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_MatchesFailMsg,
                value,
                pattern,
                message).Trim();

        /// <summary>
        /// Nulls the parameter to assert.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string NullParameterToAssert(string parameterName, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_NullParameterToAssert,
                parameterName,
                message).Trim();

        /// <summary>
        /// Endses the with fail MSG.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="substring">The substring.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string StartsWithFailMsg(string value, string substring, string message) =>
            string.Format(
                CultureInfo.CurrentCulture,
                Resources.FrameworkMessage_StartsWithFailMsg,
                value,
                substring,
                message).Trim();

        /// <summary>
        /// Unexpecteds the exception.
        /// </summary>
        /// <param name="expectedExceptionType">Expected type of the exception.</param>
        /// <param name="actualExceptionType">Actual type of the exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        internal static string UnexpectedException(Type expectedExceptionType, Type actualExceptionType, string message) =>
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.FrameworkMessage_UnexpectedException,
                        expectedExceptionType,
                        actualExceptionType,
                        message).Trim();

        /// <summary>
        /// Unexpecteds the type at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <param name="message">The user message.</param>
        /// <returns>The formatted message.</returns>
        internal static string UnexpectedTypeAt(int index, Type expectedType, Type actualType, string message) =>
            Format(
                Resources.FrameworMessage_UnexpectedTypeAt,
                index,
                expectedType,
                actualType,
                message);

        private static string Format(string format, params object[] args) =>
            string.Format(CultureInfo.CurrentCulture, format, args).Trim();
    }
}