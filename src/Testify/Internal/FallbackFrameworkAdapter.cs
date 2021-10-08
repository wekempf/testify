namespace Testify.Internal;

/// <summary>
/// Framework adapter used when the actual test framework cannot be determined.
/// </summary>
internal class FallbackFrameworkAdapter : FrameworkAdapter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FallbackFrameworkAdapter"/> class.
    /// </summary>
    public FallbackFrameworkAdapter()
        : base("Tapestry.Assertions", "Tapestry.Testing.AssertFailedException")
    {
    }

    /// <inheritdoc/>
    public override Exception CreateFrameworkException(string message) => new AssertFailedException(message);

    /// <inheritdoc/>
    protected override Assembly? GetAssembly() => typeof(FallbackFrameworkAdapter).Assembly;
}
