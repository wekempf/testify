namespace Testify.Internal;

using System;
using System.Reflection;

internal class FallbackFrameworkAdapter : FrameworkAdapter
{
    public FallbackFrameworkAdapter()
        : base("Tapestry.Assertions", "Tapestry.Testing.AssertFailedException")
    {
    }

    public override Exception CreateFrameworkException(string message) => new AssertionException(message);

    protected override Assembly? GetAssembly() => typeof(FallbackFrameworkAdapter).Assembly;
}
