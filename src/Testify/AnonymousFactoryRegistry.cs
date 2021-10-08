namespace Testify;

/// <summary>
/// Default implementation of the <see cref="IAnonymousFactoryRegistry"/> type.
/// </summary>
internal class AnonymousFactoryRegistry : IAnonymousFactoryRegistry
{
    private readonly IAnonymousFactoryRegistry? parent;
    private readonly IDictionary<Type, AnonymousFactory> factories = new Dictionary<Type, AnonymousFactory>();

    /// <summary>
    /// Initializes a new instance of the <see cref="AnonymousFactoryRegistry"/> class.
    /// </summary>
    /// <param name="parent">The parent registry.</param>
    public AnonymousFactoryRegistry(IAnonymousFactoryRegistry? parent = null) => this.parent = parent;

    /// <inheritdoc/>
    public AnonymousFactory? GetFactory(Type type)
    {
        if (factories.TryGetValue(type, out var factory))
        {
            return factory;
        }

        if (parent?.GetFactory(type) is AnonymousFactory parentFactory)
        {
            return parentFactory;
        }

        if (type.IsGenericType)
        {
            return GetFactory(type.GetGenericTypeDefinition());
        }

        return null;
    }

    /// <inheritdoc/>
    public IAnonymousFactoryRegistry Register(Type type, AnonymousFactory factory)
    {
        factories[type] = factory;
        return this;
    }
}
