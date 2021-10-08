namespace Testify
{
    /// <summary>
    /// Indicates a test ensures a reported bug was fixed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class BugAttribute : CategoryAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BugAttribute"/> class with the specified bug tracker ID.
        /// </summary>
        /// <param name="id">The bug tracker ID.</param>
        /// <remarks>
        /// This sets the "BugId [{id}]" trait in addition to the "Bug" trait.
        /// </remarks>
        public BugAttribute(string? id = null)
            : base("Bug")
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BugAttribute"/> class with the specified bug tracker ID.
        /// </summary>
        /// <param name="id">The bug tracker ID.</param>
        /// <remarks>
        /// This sets the "BugId [{id}]" trait in addition to the "Bug" trait.
        /// </remarks>
        public BugAttribute(int id)
            : this(id.ToString())
        {
        }

        /// <summary>
        /// Gets the bug tracker ID if any.
        /// </summary>
        [CustomTraitAttribute("BugId")]
        public string? Id { get; }
    }
}
