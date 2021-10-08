using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a property provides a custom test trait.
    /// </summary>
    /// <remarks>
    /// This attribute is used when creating a <see cref="CustomTraitsAttribute"/> derived attribute class.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomTraitAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTraitAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the custom trait. If not supplied the property name is used instead.</param>
        public CustomTraitAttribute(string? name = null)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the custom trait. If not supplied the property name is used instead.
        /// </summary>
        public string? Name { get; }
    }
}
