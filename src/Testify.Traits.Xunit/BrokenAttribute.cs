using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is broken and is expected to fail if run.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BrokenAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrokenAttribute"/> class.
        /// </summary>
        public BrokenAttribute()
            : base("Broken")
        {
        }
    }
}
