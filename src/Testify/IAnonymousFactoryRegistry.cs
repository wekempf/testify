namespace Testify;

/// <summary>
/// Factory for creating anonymous data.
/// </summary>
/// <param name="anon">The <see cref="IAnonymousData"/> instance used to create random data.</param>
/// <param name="type">The actual type being requested to create, usable by open generic type factories.</param>
/// <returns>A randomly generated anonymous value.</returns>
public delegate object? AnonymousFactory(IAnonymousData anon, Type type);

/// <summary>
/// Provides a registry for anonymous data factories.
/// </summary>
public interface IAnonymousFactoryRegistry
{
    /// <summary>
    /// Registers an <see cref="AnonymousFactory"/> for the specified type.
    /// </summary>
    /// <param name="type">The type associated with the <see cref="AnonymousFactory"/> being registered.</param>
    /// <param name="factory">The factory used to create an instance of the registered type.</param>
    /// <returns>The <see cref="IAnonymousFactoryRegistry"/> instance for chained registrations.</returns>
    public IAnonymousFactoryRegistry Register(Type type, AnonymousFactory factory);

    /// <summary>
    /// Gets the registered anonymous data factory, if any.
    /// </summary>
    /// <param name="type">The type factory being requested.</param>
    /// <returns>The registered factory, if any; otherwise <see langword="null"/>.</returns>
    public AnonymousFactory? GetFactory(Type type);
}
