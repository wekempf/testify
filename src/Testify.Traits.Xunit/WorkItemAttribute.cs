using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is related to a work item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class WorkItemAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkItemAttribute"/> class.
        /// </summary>
        /// <param name="id">The work item identifier.</param>
        public WorkItemAttribute(string id)
            : base("WorkItem")
        {
            ArgumentNullException.ThrowIfNull(id);
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkItemAttribute"/> class.
        /// </summary>
        /// <param name="id">The work item identifier.</param>
        public WorkItemAttribute(int id)
            : this(id.ToString())
        {
        }

        /// <summary>
        /// Gets the work item identifier.
        /// </summary>
        [CustomTrait("WorkItemId")]
        public string Id { get; }
    }
}
