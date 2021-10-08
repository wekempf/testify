namespace Testify.Internal;

/// <summary>
/// Framework adapter for the Xunit test framework.
/// </summary>
internal class XunitFrameworkAdapter : FrameworkAdapter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XunitFrameworkAdapter"/> class.
    /// </summary>
    public XunitFrameworkAdapter()
        : base("xunit.assert", "Xunit.Sdk.XunitException")
    {
    }

    /// <inheritdoc/>
    protected override Assembly? GetAssembly()
    {
        try
        {
            var assembly = Assembly.Load(new AssemblyName(AssemblyName));

            return assembly;
        }
        catch
        {
            return null;
        }
    }
}
