using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is an integration test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationAttribute"/> class.
        /// </summary>
        public IntegrationAttribute()
            : base("Integration")
        {
        }
    }
}
