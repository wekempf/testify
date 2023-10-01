namespace Testify.Internal;

internal class MstestFrameworkAdapter : FrameworkAdapter
{
    public MstestFrameworkAdapter()
        : base(
            "Microsoft.VisualStudio.TestPlatform.TestFramework",
            "Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException")
    {
    }
}
