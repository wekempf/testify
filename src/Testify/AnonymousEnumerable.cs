namespace Testify;

/// <summary>
/// Extension methods for anonymous <see cref="IEnumerable{T}"/> type.
/// </summary>
public static class AnonymousEnumerable
{
    /// <summary>
    /// Creates an enumerable of a length in the specified range of items using the specified factory.
    /// </summary>
    /// <typeparam name="T">The type of the items to generate.</typeparam>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="factory">The item factory.</param>
    /// <param name="minLength">The minimum length of the enumerable.</param>
    /// <param name="maxLength">The maximum length of the enumerable.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of random values.</returns>
    public static IEnumerable<T> AnyEnumerable<T>(this IAnonymousData anon, Func<IAnonymousData, T>? factory = default, int minLength = 2, int maxLength = 10)
    {
        Guard.Against.Null(anon);
        Guard.Against.OutOfRange(minLength, int.MinValue, int.MaxValue);
        Guard.Against.OutOfRange(maxLength, minLength + 1, int.MaxValue);

        var length = anon.AnyInt32(minLength, maxLength);
        return anon.AnyInfiniteEnumerable<T>().Take(length);
    }

    /// <summary>
    /// Creates an infinite enumerable of items using the specified factory.
    /// </summary>
    /// <typeparam name="T">The type of the items to generate.</typeparam>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="factory">The item factory.</param>
    /// <returns>An infinite <see cref="IEnumerable{T}"/> of random values.</returns>
    public static IEnumerable<T> AnyInfiniteEnumerable<T>(this IAnonymousData anon, Func<IAnonymousData, T>? factory = default)
    {
        Guard.Against.Null(anon);

        factory ??= a => a.Any<T>();

        return Generate();

        IEnumerable<T> Generate()
        {
            while (true)
            {
                yield return factory.Invoke(anon);
            }
        }
    }

    /// <summary>
    /// Returns a random item from the specified collection.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="items">The collection.</param>
    /// <returns>A random item from the collection.</returns>
    public static T AnyItem<T>(this IAnonymousData anon, params T[] items)
    {
        Guard.Against.Null(anon);

        var index = anon.AnyInt32(0, items.Length);
        return items[index];
    }

    /// <summary>
    /// Returns a random item from the specified collection.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="anon">The anonymous data provider to use.</param>
    /// <param name="items">The collection.</param>
    /// <returns>A random item from the collection.</returns>
    public static T AnyItem<T>(this IAnonymousData anon, IEnumerable<T> items)
    {
        Guard.Against.Null(anon);

        var list = items as IList<T> ?? items.ToList();
        var index = anon.AnyInt32(0, list.Count);
        return list[index];
    }

    /// <summary>
    /// Register enumerable types.
    /// </summary>
    /// <param name="registry">The registry instance.</param>
    internal static void Register(IAnonymousFactoryRegistry registry)
    {
        registry.Register(typeof(IEnumerable<>), (anon, type) =>
        {
            var itemType = type.GetGenericArguments()[0];
            var genericEnumerableFactory = typeof(AnonymousEnumerable).GetMethod(nameof(EnumerableFactory), BindingFlags.NonPublic | BindingFlags.Static) !;
            var enumerableFactory = genericEnumerableFactory.MakeGenericMethod(itemType) !;
            return enumerableFactory.Invoke(null, new[] { anon });
        });
    }

    private static IEnumerable<T> EnumerableFactory<T>(IAnonymousData anon)
        => anon.AnyEnumerable(anon => anon.Any<T>());
}
