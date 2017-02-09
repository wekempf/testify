using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Represents the actual value in an assertion statement.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    /// <remarks>
    /// <para>The <see cref="ActualValue{T}"/> type represents the actual value in an assertion
    /// statement and provides a strongly typed source for fluent assertion methods.</para>
    /// <para>This type is an advanced type and is decorated with the <see cref="EditorBrowsableAttribute"/>
    /// to remove it from IntelliSense.</para>
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ActualValue<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActualValue{T}"/> class.
        /// </summary>
        /// <param name="value">The actual value.</param>
        public ActualValue(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the actual value.
        /// </summary>
        /// <value>
        /// The actual value.
        /// </value>
        public T Value { get; }
    }
}