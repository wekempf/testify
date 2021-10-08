namespace Testify;

/// <summary>
/// Provides extension methods for the <see cref="IAnonymousFactoryRegistry"/> type.
/// </summary>
public static class AnonymousFactoryRegistryExtensions
{
    /// <summary>
    /// Invokes a registry method to register types on <paramref name="registry"/>.
    /// </summary>
    /// <param name="registry">The <see cref="IAnonymousFactoryRegistry"/> instance.</param>
    /// <param name="register">The registry method.</param>
    /// <returns>The registry instance for chained calls.</returns>
    public static IAnonymousFactoryRegistry Register(this IAnonymousFactoryRegistry registry, Action<IAnonymousFactoryRegistry> register)
    {
        Guard.Against.Null(registry);
        Guard.Against.Null(register);

        register.Invoke(registry);
        return registry;
    }

    /// <summary>
    /// Registers an <see cref="AnonymousFactory"/> for the specified type.
    /// </summary>
    /// <typeparam name="T">The type associated with the <see cref="AnonymousFactory"/> being registered.</typeparam>
    /// <param name="registry">The <see cref="IAnonymousFactoryRegistry"/> instance.</param>
    /// <param name="factory">The factory used to create an instance of the registered type.</param>
    /// <returns>The <see cref="IAnonymousFactoryRegistry"/> instance for chained registrations.</returns>
    public static IAnonymousFactoryRegistry Register<T>(this IAnonymousFactoryRegistry registry, AnonymousFactory factory)
    {
        Guard.Against.Null(registry);
        Guard.Against.Null(factory);

        return registry.Register(typeof(T), factory);
    }

    /// <summary>
    /// Registers an <see cref="AnonymousFactory"/> for the specified type.
    /// </summary>
    /// <typeparam name="T">The type associated with the <see cref="AnonymousFactory"/> being registered.</typeparam>
    /// <param name="registry">The <see cref="IAnonymousFactoryRegistry"/> instance.</param>
    /// <param name="factory">The factory used to create an instance of the registered type.</param>
    /// <returns>The <see cref="IAnonymousFactoryRegistry"/> instance for chained registrations.</returns>
    public static IAnonymousFactoryRegistry Register<T>(this IAnonymousFactoryRegistry registry, Func<IAnonymousData, Type, T> factory)
    {
        Guard.Against.Null(registry);
        Guard.Against.Null(factory);

        return registry.Register(typeof(T), (anon, type) => factory.Invoke(anon, type));
    }
}
