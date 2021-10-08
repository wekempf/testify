namespace Testify;

/// <summary>
/// Represents text that should not be quoted when formatting.
/// </summary>
internal class Text
{
    private readonly string value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Text"/> class.
    /// </summary>
    /// <param name="value">The text value.</param>
    public Text(string value) => this.value = value;

    /// <inheritdoc/>
    public override string ToString() => value;
}
