﻿namespace Testify;

/// <summary>
/// Defines an interface for a factory object that can be used to create other objects.
/// </summary>
public interface IAnonymousData
{
    /// <summary>
    /// Creates an object of the specified type.
    /// </summary>
    /// <param name="type">The type of object to create.</param>
    /// <param name="populateOption">The populate option.</param>
    /// <returns>
    /// An instance of the specified type.
    /// </returns>
    /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
    object? Any(Type type, PopulateOption populateOption = PopulateOption.None);

    /// <summary>
    /// Creates a random <see langword="double"/> value within the specified range using the specified
    /// distribution algorithm.
    /// </summary>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <param name="distribution">The distribution algorithm to use.</param>
    /// <returns>A random <see langword="double"/> value.</returns>
    /// <remarks>
    /// Overrides should treat <see langword="null"/> values for <paramref name="distribution"/> as
    /// <see cref="P:Distribution.Uniform"/>.
    /// </remarks>
    double AnyDouble(double minimum = double.MinValue, double maximum = double.MaxValue, Distribution? distribution = default);

    /// <summary>
    /// Populates the specified instance by assigning all properties to anonymous values.
    /// </summary>
    /// <typeparam name="TInstance">The type of the instance to populate.</typeparam>
    /// <param name="instance">The instance to populate.</param>
    /// <param name="deep">If set to <see langword="true" /> then properties are assigned recursively, populating
    /// the entire object tree.</param>
    /// <returns>The populated instance.</returns>
    TInstance Populate<TInstance>(TInstance instance, bool deep);
}
