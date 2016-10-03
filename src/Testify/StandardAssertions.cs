using System;
using System.ComponentModel;
using static Testify.Assertions;
using static Testify.FrameworkMessages;

namespace Testify
{
    /// <summary>
    /// Defines standard assertion methods used with the fluent <see cref="Assertions.Assert{T}(T)"/> method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class StandardAssertions
    {
        /// <summary>
        /// Verifies that two specified doubles are equal or within the specified accuracy of each other.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<double> actualValue, double expected, double delta)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, delta, null, null);
        }

        /// <summary>
        /// Verifies that two specified doubles are equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<double> actualValue, double expected, double delta, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, delta, message, null);
        }

        /// <summary>
        /// Verifies that two specified doubles are equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<double> actualValue, double expected, double delta, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (double.IsNaN(expected) || double.IsNaN(actualValue.Value) || double.IsNaN(delta))
            {
                Throw(nameof(IsEqualTo), IsEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
            }

            if (Math.Abs(expected - actualValue.Value) <= delta)
            {
                return;
            }

            Throw(nameof(IsEqualTo), IsEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified singles are equal or within the specified accuracy of each other.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<float> actualValue, float expected, float delta)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, delta, null, null);
        }

        /// <summary>
        /// Verifies that two specified singles are equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<float> actualValue, float expected, float delta, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, delta, message, null);
        }

        /// <summary>
        /// Verifies that two specified singles are equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo(this ActualValue<float> actualValue, float expected, float delta, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (float.IsNaN(expected) || float.IsNaN(actualValue.Value) || float.IsNaN(delta))
            {
                Throw(nameof(IsEqualTo), IsEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
            }

            if (Math.Abs(expected - actualValue.Value) <= delta)
            {
                return;
            }

            Throw(nameof(IsEqualTo), IsEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified objects are equal.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, object expected)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified objects are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, object expected, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified objects are equal. Displays a message if the assertion fails,
        /// and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, object expected, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (object.Equals(expected, actualValue.Value))
            {
                return;
            }

            var assertionMessage = (object)actualValue.Value == null || expected == null || actualValue.Value.GetType().Equals(expected.GetType())
                    ? IsEqualToFailMsg(ReplaceNulls(expected), ReplaceNulls(actualValue.Value))
                    : IsEqualToDifferentTypesFailMsg(ReplaceNulls(expected), expected.GetType().FullName, ReplaceNulls(actualValue.Value), actualValue.Value.GetType().FullName);
            Throw(nameof(IsEqualTo), assertionMessage, message, parameters);
        }

        /// <summary>
        /// Verifies that two specified objects are equal.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, T expected)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified objects are equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, T expected, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsEqualTo(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified objects are equal. Displays a message if the assertion fails,
        /// and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> and <paramref name="expected"/> are not equal.</exception>
        public static void IsEqualTo<T>(this ActualValue<T> actualValue, T expected, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (object.Equals(expected, actualValue.Value))
            {
                return;
            }

            var finalMessage = (object)actualValue.Value == null || (object)expected == null || actualValue.Value.GetType().Equals(expected.GetType())
                ? IsEqualToFailMsg(ReplaceNulls(expected), ReplaceNulls(actualValue.Value))
                : IsEqualToDifferentTypesFailMsg(ReplaceNulls(expected), expected.GetType().FullName, ReplaceNulls(actualValue.Value), actualValue.Value.GetType().FullName);
            Throw(nameof(IsEqualTo), finalMessage, message, parameters);
        }

        /// <summary>
        /// Asserts that the actual value is <c>false</c>.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is <c>true</c>.</exception>
        public static void IsFalse(this ActualValue<bool> actualValue)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsFalse(null, null);
        }

        /// <summary>
        /// Asserts that the actual value is <c>false</c>. Displays a message if the assertion
        /// fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is <c>true</c>.</exception>
        public static void IsFalse(this ActualValue<bool> actualValue, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsFalse(message, null);
        }

        /// <summary>
        /// Asserts that the actual value is <c>false</c>. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is <c>true</c>.</exception>
        public static void IsFalse(this ActualValue<bool> actualValue, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (actualValue.Value)
            {
                Throw(nameof(IsFalse), null, message, parameters);
            }
        }

        /// <summary>
        /// Verifies that the specified object is an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> or <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is not an instance of <paramref name="expectedType"/>.</exception>
        public static void IsInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            actualValue.IsInstanceOfType(expectedType, null, null);
        }

        /// <summary>
        /// Verifies that the specified object is an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> or <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is not an instance of <paramref name="expectedType"/>.</exception>
        public static void IsInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            actualValue.IsInstanceOfType(expectedType, message, null);
        }

        /// <summary>
        /// Verifies that the specified object is an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> or <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is not an instance of <paramref name="expectedType"/>.</exception>
        public static void IsInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            if (expectedType.IsInstanceOfType(actualValue.Value))
            {
                return;
            }

            Throw(nameof(IsInstanceOfType), IsInstanceOfTypeFailMsg(expectedType, actualValue.Value), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified doubles are not equal or within the specified accuracy of each
        /// other.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<double> actualValue, double expected, double delta)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, delta, null, null);
        }

        /// <summary>
        /// Verifies that two specified doubles are not equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<double> actualValue, double expected, double delta, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, delta, message, null);
        }

        /// <summary>
        /// Verifies that two specified doubles are not equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<double> actualValue, double expected, double delta, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (double.IsNaN(expected) || double.IsNaN(actualValue.Value) || double.IsNaN(delta))
            {
                Throw(nameof(IsNotEqualTo), IsNotEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
            }

            if (Math.Abs(expected - actualValue.Value) > delta)
            {
                return;
            }

            Throw(nameof(IsNotEqualTo), IsNotEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified singles are not equal or within the specified accuracy of each other.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<float> actualValue, float expected, float delta)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, delta, null, null);
        }

        /// <summary>
        /// Verifies that two specified singles are not equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<float> actualValue, float expected, float delta, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, delta, message, null);
        }

        /// <summary>
        /// Verifies that two specified singles are not equal or within the specified accuracy of each other.
        /// Displays a message if the assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="delta">The required accuracy. The assertion will fail only if <paramref name="expected"/>
        /// is different from <paramref name="actualValue"/> by more than <paramref name="delta"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo(this ActualValue<float> actualValue, float expected, float delta, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (float.IsNaN(expected) || float.IsNaN(actualValue.Value) || float.IsNaN(delta))
            {
                Throw(nameof(IsNotEqualTo), IsNotEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
            }

            if (Math.Abs(expected - actualValue.Value) > delta)
            {
                return;
            }

            Throw(nameof(IsNotEqualTo), IsNotEqualToDeltaFailMsg(expected, actualValue.Value, delta), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, object expected)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal. Displays a message if the assertion fails.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, object expected, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal. Displays a message if the assertion fails,
        /// and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, object expected, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (!object.Equals(expected, actualValue.Value))
            {
                return;
            }

            Throw(nameof(IsNotEqualTo), IsNotEqualToFailMsg(expected, actualValue.Value), message, parameters);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, T expected)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, T expected, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotEqualTo(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified objects are not equal. Displays a message if the assertion fails,
        /// and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The actual value type.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is equal to <paramref name="expected"/>.</exception>
        public static void IsNotEqualTo<T>(this ActualValue<T> actualValue, T expected, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (!object.Equals(expected, actualValue.Value))
            {
                return;
            }

            Throw(nameof(IsNotEqualTo), IsNotEqualToFailMsg(expected, actualValue.Value), message, parameters);
        }

        /// <summary>
        /// Verifies that the specified object is not an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type not expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> or <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is an instance of <paramref name="expectedType"/>.</exception>
        public static void IsNotInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            actualValue.IsNotInstanceOfType(expectedType, null, null);
        }

        /// <summary>
        /// Verifies that the specified object is not an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type not expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> or <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is an instance of <paramref name="expectedType"/>.</exception>
        public static void IsNotInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            actualValue.IsNotInstanceOfType(expectedType, message, null);
        }

        /// <summary>
        /// Verifies that the specified object is not an instance of the specified type. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expectedType">The type not expected to be found in the inheritance hierarchy of
        /// <paramref name="actualValue.Value"/>.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> <paramref name="expectedType"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/> is an instance of <paramref name="expectedType"/>.</exception>
        public static void IsNotInstanceOfType<T>(this ActualValue<T> actualValue, Type expectedType, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));
            Argument.NotNull(expectedType, nameof(expectedType));

            if (!expectedType.IsInstanceOfType(actualValue.Value))
            {
                return;
            }

            Throw(nameof(IsNotInstanceOfType), IsNotInstanceOfTypeFailMsg(expectedType, actualValue.Value), message, parameters);
        }

        /// <summary>
        /// Verifies that the specified object is not <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>null</c>.</exception>
        public static void IsNotNull<T>(this ActualValue<T> actualValue)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotNull(null, null);
        }

        /// <summary>
        /// Verifies that the specified object is not <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>null</c>.</exception>
        public static void IsNotNull<T>(this ActualValue<T> actualValue, string message)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotNull(message, null);
        }

        /// <summary>
        /// Verifies that the specified object is not <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>null</c>.</exception>
        public static void IsNotNull<T>(this ActualValue<T> actualValue, string message, params object[] parameters)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (actualValue.Value != null)
            {
                return;
            }

            Throw(nameof(IsNotNull), null, message, parameters);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to different objects. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is the same as <paramref name="expected"/>.</exception>
        public static void IsNotSameAs<T>(this ActualValue<T> actualValue, T expected)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotSameAs(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to different objects. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is the same as <paramref name="expected"/>.</exception>
        public static void IsNotSameAs<T>(this ActualValue<T> actualValue, T expected, string message)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNotSameAs(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to different objects. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is the same as <paramref name="expected"/>.</exception>
        public static void IsNotSameAs<T>(this ActualValue<T> actualValue, T expected, string message, params object[] parameters)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (!object.ReferenceEquals(actualValue.Value, expected))
            {
                return;
            }

            Throw(nameof(IsNotSameAs), null, message, parameters);
        }

        /// <summary>
        /// Verifies that the specified object is <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not <c>null</c>.</exception>
        public static void IsNull<T>(this ActualValue<T> actualValue)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNull(null, null);
        }

        /// <summary>
        /// Verifies that the specified object is <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not <c>null</c>.</exception>
        public static void IsNull<T>(this ActualValue<T> actualValue, string message)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsNull(message, null);
        }

        /// <summary>
        /// Verifies that the specified object is <c>null</c>. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual value.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not <c>null</c>.</exception>
        public static void IsNull<T>(this ActualValue<T> actualValue, string message, params object[] parameters)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (actualValue.Value == null)
            {
                return;
            }

            Throw(nameof(IsNull), null, message, parameters);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to the same object.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not the same as <paramref name="expected"/>.</exception>
        public static void IsSameAs<T>(this ActualValue<T> actualValue, T expected)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsSameAs(expected, null, null);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to the same object. Displays a message if the
        /// assertion fails.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not the same as <paramref name="expected"/>.</exception>
        public static void IsSameAs<T>(this ActualValue<T> actualValue, T expected, string message)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsSameAs(expected, message, null);
        }

        /// <summary>
        /// Verifies that two specified object variables refer to the same object. Displays a message if the
        /// assertion fails, and applies the specified formatting to it.
        /// </summary>
        /// <typeparam name="T">The type of the actual object.</typeparam>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is not the same as <paramref name="expected"/>.</exception>
        public static void IsSameAs<T>(this ActualValue<T> actualValue, T expected, string message, params object[] parameters)
            where T : class
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (object.ReferenceEquals(actualValue.Value, expected))
            {
                return;
            }

            Throw(nameof(IsSameAs), null, message, parameters);
        }

        /// <summary>
        /// Asserts that the actual value is <c>true</c>.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>false</c>.</exception>
        public static void IsTrue(this ActualValue<bool> actualValue)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsTrue(null, null);
        }

        /// <summary>
        /// Asserts that the actual value is <c>true</c>. Displays a message if the assertion
        /// fails.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>false</c>.</exception>
        public static void IsTrue(this ActualValue<bool> actualValue, string message)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            actualValue.IsTrue(message, null);
        }

        /// <summary>
        /// Asserts that the actual value is <c>true</c>. Displays a message if the assertion
        /// fails, and applies the specified formatting to it.
        /// </summary>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actualValue"/> is <c>null</c>.</exception>
        /// <exception cref="AssertionException"><paramref name="actualValue"/>'s value is <c>false</c>.</exception>
        public static void IsTrue(this ActualValue<bool> actualValue, string message, params object[] parameters)
        {
            Argument.NotNull(actualValue, nameof(actualValue));

            if (!actualValue.Value)
            {
                Throw(nameof(IsTrue), null, message, parameters);
            }
        }
    }
}