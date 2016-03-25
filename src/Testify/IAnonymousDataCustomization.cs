namespace Testify
{
    /// <summary>
    /// Defines an interface used to customize the <see cref="AnonymousData"/> behavior.
    /// </summary>
    public interface IAnonymousDataCustomization
    {
        /// <summary>
        /// Creates an object of a specified type.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if an object was created, otherwise <c>false</c>.</returns>
        bool Create(IAnonymousDataContext context, out object result);
    }
}