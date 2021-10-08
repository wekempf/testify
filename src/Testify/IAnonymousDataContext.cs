namespace Testify;

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
    /// <returns><see langword="true"/> if an object was created, otherwise <see langword="false"/>.</returns>
    bool CallNextCustomization(out object result);
}
