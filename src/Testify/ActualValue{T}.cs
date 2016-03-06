using System.ComponentModel;

namespace Testify
{
    /// <summary>
    /// Represents the actual value in an assertion statement.
    /// </summary>
    /// <typeparam name="T">The type of the actual value.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ActualValue<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActualValue{T}"/> class.
        /// </summary>
        /// <param name="value">The actual value.</param>
        public ActualValue(T value)
        {
            this.Value = value;
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