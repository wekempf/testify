namespace Testify;

/// <summary>
/// Extension methods for <see cref="IAnonymousData"/>.
/// </summary>
public static class AnonymousDataExtensions
{
    /// <summary>
    /// Creates an object of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <param name="anon">The <see cref="IAnonymousData"/> instance.</param>
    /// <param name="populateOption">The populate option.</param>
    /// <returns>
    /// An instance of the specified type.
    /// </returns>
    /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
    public static T Any<T>(this IAnonymousData anon, PopulateOption populateOption = PopulateOption.None) => (T)anon.Any(typeof(T), populateOption) !;
}
