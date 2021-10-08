using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit.Abstractions;
using Xunit.Sdk;

namespace Testify
{
    /// <summary>
    /// <see cref="ITraitDiscoverer"/> that finds custom traits defined by the <see cref="CustomTraitsAttribute"/>.
    /// </summary>
    internal class CustomTraitsDiscoverer : ITraitDiscoverer
    {
        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var traits = traitAttribute.GetNamedArgument<IEnumerable<KeyValuePair<string, string>>>("Traits");
            foreach (var trait in traits)
            {
                yield return trait;
            }
        }
    }
}
