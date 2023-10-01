namespace Testify.Internal;

internal class NunitFrameworkAdapter : FrameworkAdapter
{
    public NunitFrameworkAdapter()
        : base("nunit.framework", "NUnit.Framework.AssertionException")
    {
    }
}
