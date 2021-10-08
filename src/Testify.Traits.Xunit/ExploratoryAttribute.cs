using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is an exploratory test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ExploratoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExploratoryAttribute"/> class.
        /// </summary>
        public ExploratoryAttribute()
            : base("Exploratory")
        {
        }
    }
}
