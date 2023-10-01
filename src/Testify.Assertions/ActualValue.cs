namespace Testify;

using System;

/// <summary>
/// Represents a record that holds actual value for an assertion and the
/// expression for it.
/// </summary>
/// <typeparam name="T">The type of the actual value.</typeparam>
public record ActualValue<T>(T? Value, string Expression) : IFormattable
{
    /// <inheritdoc/>
    public override string ToString()
    {
        return ToString(null, null);
    }

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => format switch
        {
            "e" => FormatExpression(Expression),
            "v" => Format(Value),
            _ => Format(Value, Expression)
        };
}
