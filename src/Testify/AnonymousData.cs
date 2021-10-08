namespace Testify;

/// <summary>
/// Provides an object factory that can be used to create anonymous values for use in unit tests.
/// </summary>
public sealed class AnonymousData : IAnonymousData, IAnonymousFactoryRegistry
{
    private readonly IAnonymousFactoryRegistry registry;
    private readonly Random random;

    static AnonymousData()
    {
        GlobalRegistry.Register((anon, _) => anon.AnyDouble());
        GlobalRegistry.Register((anon, _) => anon.AnyInt64());
        GlobalRegistry.Register((anon, _) => anon.AnyAlphaNumericChar());
        GlobalRegistry.Register((anon, _) => anon.AnyBool());
        GlobalRegistry.Register(AnonymousEnumerable.Register);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnonymousData"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor initializes the <see cref="AnonymousData"/> instance with a constant seed value.
    /// </para>
    /// <para>
    /// <see cref="AnonymousData"/> generates random objects. While the intent is for only values that you intend
    /// to not have any impact on your test it is still a best practice to create a new instance per test to
    /// ensure consistent test runs. You can factor out the creation and configuration instead of sharing an
    /// instance in order to use the same configuration in multiple tests.
    /// </para>
    /// </remarks>
    public AnonymousData()
        : this(0x07357FAC)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnonymousData"/> class.
    /// </summary>
    /// <param name="seed">The seed to provide to the random number generator.</param>
    /// <remarks>
    /// <para>
    /// <see cref="AnonymousData"/> generates random objects. While the intent is for only values that you intend
    /// to not have any impact on your test it is still a best practice to create a new instance per test to
    /// ensure consistent test runs. You can factor out the creation and configuration instead of sharing an
    /// instance in order to use the same configuration in multiple tests.
    /// </para>
    /// </remarks>
    public AnonymousData(int seed)
    {
        random = new Random(seed);
        registry = new AnonymousFactoryRegistry(GlobalRegistry);
    }

    /// <summary>
    /// Gets the global registry for anonymous data factories.
    /// </summary>
    public static IAnonymousFactoryRegistry GlobalRegistry { get; } = new AnonymousFactoryRegistry();

    /// <inheritdoc/>
    public object? Any(Type type, PopulateOption populateOption = PopulateOption.None)
    {
        var factory = GetFactory(type);
        if (factory == null)
        {
            throw new InvalidOperationException($"Unable to create instance of type {type}.");
        }

        object? instance = null;
        try
        {
            instance = factory.Invoke(this, type);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unable to create instance of type {type}.", ex);
        }

        if (instance != null && populateOption != PopulateOption.None)
        {
            instance = Populate(instance, populateOption == PopulateOption.Deep);
        }

        return instance;
    }

    /// <inheritdoc/>
    public double AnyDouble(double minimum = float.MinValue, double maximum = float.MaxValue, Distribution? distribution = default)
    {
        _ = maximum > minimum ? maximum : throw new ArgumentOutOfRangeException(nameof(maximum), "The maximum value must be greater than the minimum value.");

        distribution = distribution ?? Distribution.Uniform;
        var halfMinimum = minimum / 2.0;
        var halfMaximum = maximum / 2.0;
        var average = halfMinimum + halfMaximum;
        var factor = maximum - average;
        return (((2.0 * distribution.NextDouble(random)) - 1.0) * factor) + average;
    }

    /// <inheritdoc/>
    public TInstance Populate<TInstance>(TInstance instance, bool deep)
    {
        var properties = instance?.GetType().GetProperties().Where(p => p.CanWrite) ?? Enumerable.Empty<PropertyInfo>();
        foreach (var prop in properties)
        {
            var value = this.Any(prop.PropertyType, deep ? PopulateOption.Deep : PopulateOption.None);
            prop.SetValue(instance, value, null);
        }

        return instance;
    }

    /// <inheritdoc/>
    public IAnonymousFactoryRegistry Register(Type type, AnonymousFactory factory) => registry.Register(type, factory);

    /// <inheritdoc/>
    public AnonymousFactory? GetFactory(Type type) => registry.GetFactory(type) ?? GetDefaultFactory(type);

    private AnonymousFactory GetDefaultFactory(Type type)
    {
        throw new NotImplementedException();
    }
}
