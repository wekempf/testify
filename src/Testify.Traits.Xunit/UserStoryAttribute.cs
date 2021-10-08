using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Indicates a test is related to a user story.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class UserStoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStoryAttribute"/> class.
        /// </summary>
        /// <param name="id">The user story identifier.</param>
        public UserStoryAttribute(string id)
            : base("UserStory")
        {
            ArgumentNullException.ThrowIfNull(id);
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStoryAttribute"/> class.
        /// </summary>
        /// <param name="id">The user story identifier.</param>
        public UserStoryAttribute(int id)
            : this(id.ToString())
        {
        }

        /// <summary>
        /// Gets the user story identifier.
        /// </summary>
        [CustomTrait("UserStoryId")]
        public string Id { get; }
    }
}
