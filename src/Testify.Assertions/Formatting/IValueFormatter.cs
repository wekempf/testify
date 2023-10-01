namespace Testify.Formatting;

/// <summary>
/// Provides a mechanism for controlling the formatting of values in assertion
/// failure messages.
/// </summary>
public interface IValueFormatter
{
    /// <summary>
    /// Determines whether this instance can format the specified
    /// <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <returns><c>true</c> if this instance can format the specified value;
    ///     otherwise, <c>false</c>.</returns>
    bool CanFormat(object? value);

    /// <summary>
    /// Formats the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <returns>The formatted string representation for the
    ///     <paramref name="value"/>.</returns>
    string Format(object? value);
}
