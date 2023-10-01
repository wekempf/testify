namespace Testify.Internal;

using System.Reflection;

internal class XunitFrameworkAdapter : FrameworkAdapter
{
    public XunitFrameworkAdapter()
        : base("xunit.assert", "Xunit.Sdk.XunitException")
    {
    }

    protected override Assembly? GetAssembly()
    {
        try
        {
            return Assembly.Load(new AssemblyName(AssemblyName));
        }
        catch
        {
            return null;
        }
    }
}
