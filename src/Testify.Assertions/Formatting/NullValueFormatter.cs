namespace Testify.Formatting;

internal class NullValueFormatter : IValueFormatter
{
    private static readonly string FormattedNullValue = "<null>";

    public bool CanFormat(object? value) => value is null;

    public string Format(object? value) => FormattedNullValue;
}
