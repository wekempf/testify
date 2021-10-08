using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;

namespace Testify
{
    /// <summary>
    /// Base class for attributes that provide custom traits to tests.
    /// </summary>
    /// <remarks>
    /// Custom trait values can either be supplied by overriding <see cref="CustomTraitsAttribute.GetTraits"/> or by decorating properties with <see cref="CustomTraitsAttribute"/>.
    /// </remarks>
    [TraitDiscoverer("Testify.CustomTraitsDiscoverer", "Testify.Traits.Xunit")]
    public abstract class CustomTraitsAttribute : Attribute, ITraitAttribute
    {
        private readonly Lazy<IEnumerable<KeyValuePair<string, string>>> traits;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTraitsAttribute"/> class.
        /// </summary>
        protected CustomTraitsAttribute()
        {
            traits = new Lazy<IEnumerable<KeyValuePair<string, string>>>(GetTraits);
        }

        /// <summary>
        /// Gets the custom traits.
        /// </summary>
        internal IEnumerable<KeyValuePair<string, string>> Traits => traits.Value;

        /// <summary>
        /// Get the custom traits.
        /// </summary>
        /// <remarks>The base implementation uses reflection to add properties marked with the <see cref="CustomTraitAttribute"/> as traits.</remarks>
        /// <returns>The collection of traits to add to the test.</returns>
        protected virtual IEnumerable<KeyValuePair<string, string>> GetTraits()
        {
            var props = this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Select(p => (Property: p, CustomTraitAttribute: p.GetCustomAttribute<CustomTraitAttribute>()))
                .Where(p => p.CustomTraitAttribute != null);

            foreach (var prop in props)
            {
                var value = prop.Property.GetValue(this)?.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var name = string.IsNullOrWhiteSpace(prop.CustomTraitAttribute?.Name)
                        ? prop.Property.Name
                        : prop.CustomTraitAttribute.Name;
                    yield return new KeyValuePair<string, string>(name, value);
                }
            }
        }
    }
}
