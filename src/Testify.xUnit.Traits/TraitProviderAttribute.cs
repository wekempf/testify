namespace Testify;

using System;
using System.Collections.Generic;

using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
[TraitDiscoverer(TraitProviderDiscoverer.TypeName, TraitProviderDiscoverer.AssemblyName)]
public abstract class TraitProviderAttribute : Attribute, ITraitProviderAttribute
{
    public abstract IEnumerable<KeyValuePair<string, string>> GetTraits();
}
