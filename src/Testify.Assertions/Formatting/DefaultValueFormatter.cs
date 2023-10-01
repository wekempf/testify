namespace Testify.Formatting;

internal class DefaultValueFormatter : IValueFormatter
{
    public bool CanFormat(object? value) => true;

    public string Format(object? value) => Guard.Against.Null(value).ToString()!;
}
