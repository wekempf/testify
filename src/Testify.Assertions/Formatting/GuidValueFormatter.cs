namespace Testify.Formatting;

internal class GuidValueFormatter : ValueFormatter<Guid>
{
    public override string Format(Guid value) => $"<{value}>";
}
