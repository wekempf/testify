using System;

namespace Testify
{
    /// <summary>
    /// Defines a context used during object creation.
    /// </summary>
    public interface IAnonymousDataContext : IAnonymousData
    {
        /// <summary>
        /// Gets the type being created.
        /// </summary>
        Type ResultType { get; }

        /// <summary>
        /// Calls the next customization in the chain.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if an object was created, otherwise <c>false</c>.</returns>
        bool CallNextCustomization(out object result);
    }
}