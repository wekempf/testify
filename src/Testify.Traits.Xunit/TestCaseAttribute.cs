using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is related to a specific test case.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class TestCaseAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseAttribute"/> class.
        /// </summary>
        /// <param name="id">The test case identifier.</param>
        public TestCaseAttribute(string id)
            : base("TestCase")
        {
            ArgumentNullException.ThrowIfNull(id);
            Id = id;
        }

        /// <summary>
        /// Gets the test case identifier.
        /// </summary>
        [CustomTrait("TestCaseId")]
        public string Id { get; }
    }
}
