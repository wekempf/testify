namespace Testify.Formatting;

/// <summary>
/// Provides methods to help with formatting of assertion failure messages.
/// </summary>
public static class Formatter
{
    private static readonly IValueFormatter NullFormatter = new NullValueFormatter();
    private static readonly IValueFormatter DefaultFormatter = new DefaultValueFormatter();

    internal static readonly List<IValueFormatter> Formatters = new();

    static Formatter()
    {
        Formatters.Add(new GuidValueFormatter());
        Formatters.Add(new StringValueFormatter());
        Formatters.Add(new DateTimeValueFormatter());
    }

    /// <summary>
    /// Formats the specified value in a manner appropriate for assertion
    /// failure messages.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="expression">The caller expression provided for the value.
    ///     </param>
    /// <returns>A formatted representation of the <paramref name="value"/> and
    ///     <paramref name="expression"/>.</returns>
    /// <remarks>
    /// Several types and values are formatted to provide better assertion
    /// messages. You can extend special formatting to other types (TODO).
    /// </remarks>
    public static string Format(object? value, string? expression = null)
    {
        var formatter = FindFormatter(value);
        var formattedValue = formatter.Format(value);

        if (!string.IsNullOrWhiteSpace(expression))
        {
            formattedValue = $"{FormatExpression(expression)} ({formattedValue})";
        }

        return formattedValue;
    }

    /// <summary>
    /// Formats the specified caller expression.
    /// </summary>
    /// <param name="expression">The caller expression.</param>
    /// <returns>The formatted caller expression.</returns>
    public static string FormatExpression(string expression)
        => $"«{expression}»";

    private static IValueFormatter FindFormatter(object? value)
    {
        if (value == null)
        {
            return NullFormatter;
        }

        for (var i = Formatters.Count - 1; i >= 0; i--)
        {
            var formatter = Formatters[i];
            if (formatter.CanFormat(value))
            {
                return formatter;
            }
        }

        return DefaultFormatter;
    }
}
