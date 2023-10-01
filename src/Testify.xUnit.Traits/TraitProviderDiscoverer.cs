namespace Testify;

using System.Collections.Generic;

using Xunit.Abstractions;
using Xunit.Sdk;

public class TraitProviderDiscoverer : ITraitDiscoverer
{
    public const string TypeName = "Testify.TraitProviderDiscoverer";
    public const string AssemblyName = "Testify.xUnit.Traits";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var attributeInfo = traitAttribute as IReflectionAttributeInfo;
        var traitProviderAttribute = attributeInfo?.Attribute as ITraitProviderAttribute;
        return traitProviderAttribute?.GetTraits() ?? Enumerable.Empty<KeyValuePair<string, string>>();
    }
}
