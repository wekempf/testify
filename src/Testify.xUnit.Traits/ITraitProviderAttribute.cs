namespace Testify;

using System.Collections.Generic;

using Xunit.Sdk;

public interface ITraitProviderAttribute : ITraitAttribute
{
    IEnumerable<KeyValuePair<string, string>> GetTraits();
}
