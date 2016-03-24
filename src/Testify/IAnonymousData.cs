using System;

namespace Testify
{
    /// <summary>
    /// Defines an interface for a factory object that can be used to create other objects.
    /// </summary>
    public interface IAnonymousData
    {
        /// <summary>
        /// Creates an object of the specified type.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
        object Any(Type type);

        /// <summary>
        /// Creates a random <see langword="double"/> value within the specified range using the specified
        /// distribution algorithm.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random <see langword="int"/> value.</returns>
        /// <remarks>
        /// Overrides should treat <c>null</c> values for <paramref name="distribution"/> as
        /// <see cref="P:Distribution.Uniform"/>.
        /// </remarks>
        double AnyDouble(double minimum, double maximum, Distribution distribution);
    }
}