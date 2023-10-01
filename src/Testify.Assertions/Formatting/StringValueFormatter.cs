namespace Testify.Formatting;

internal class StringValueFormatter : ValueFormatter<string>
{
    public override string Format(string value) => $"\"{value}\"";
}
