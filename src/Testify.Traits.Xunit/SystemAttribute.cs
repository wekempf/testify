using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is a system test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SystemAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAttribute"/> class.
        /// </summary>
        public SystemAttribute()
            : base("System")
        {
        }
    }
}
