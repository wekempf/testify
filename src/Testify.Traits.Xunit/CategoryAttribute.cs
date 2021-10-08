using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit.Sdk;

namespace Testify
{
    /// <summary>
    /// Indicates a test is in the specified category.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CategoryAttribute : CustomTraitsAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAttribute"/> class.
        /// </summary>
        /// <param name="name">The category name.</param>
        public CategoryAttribute(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            Name = name;
        }

        /// <summary>
        /// Gets the category name.
        /// </summary>
        [CustomTraitAttribute("Category")]
        public string Name { get; }
    }
}
