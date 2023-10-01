namespace Testify.Formatting;

/// <summary>
/// Base class for <see cref="IValueFormatter"/> types that can format the specified <typeparamref name="T"/> type.
/// </summary>
/// <typeparam name="T">The type to format.</typeparam>
/// <seealso cref="Testify.Formatting.IValueFormatter" />
public abstract class ValueFormatter<T> : IValueFormatter
{
    /// <inheritdoc/>
    public virtual bool CanFormat(object? value) => value is T;

    /// <summary>
    /// Formats the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <returns>The formatted string representation for the
    ///     <paramref name="value"/>.</returns>
    public abstract string Format(T value);

    /// <inheritdoc/>
    string IValueFormatter.Format(object? value) => Format((T)value!);
}
