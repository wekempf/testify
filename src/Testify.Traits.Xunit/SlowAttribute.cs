using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is slow to execute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SlowAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlowAttribute"/> class.
        /// </summary>
        public SlowAttribute()
            : base("Slow")
        {
        }
    }
}
