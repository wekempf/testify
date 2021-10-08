using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is a unit test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UnitAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAttribute"/> class.
        /// </summary>
        public UnitAttribute()
            : base("Unit")
        {
        }
    }
}
