using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is related to a feature.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class FeatureAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureAttribute"/> class.
        /// </summary>
        /// <param name="id">The feature identifier.</param>
        public FeatureAttribute(string id)
            : base("Feature")
        {
            ArgumentNullException.ThrowIfNull(id);
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureAttribute"/> class.
        /// </summary>
        /// <param name="id">The feature identifier.</param>
        public FeatureAttribute(int id)
            : this(id.ToString())
        {
        }

        /// <summary>
        /// Gets the feature identifier.
        /// </summary>
        [CustomTrait("FeatureId")]
        public string Id { get; }
    }
}
