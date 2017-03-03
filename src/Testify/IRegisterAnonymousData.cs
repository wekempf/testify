using System;
using System.Reflection;

namespace Testify
{
    /// <summary>
    /// Defines the interface used to register factory methods on an <see cref="AnonymousData"/>.
    /// </summary>
    public interface IRegisterAnonymousData
    {
        /// <summary>
        /// Register a factory method for the specified type.
        /// </summary>
        /// <param name="type">The type of object the factory method creates.</param>
        /// <param name="factory">The factory method.</param>
        void Register(Type type, Func<IAnonymousData, object> factory);

        /// <summary>
        /// Registers a factory method for the specified property.
        /// </summary>
        /// <param name="property">The property to populate.</param>
        /// <param name="factory">The factory method.</param>
        void Register(PropertyInfo property, Func<IAnonymousData, object> factory);

        /// <summary>
        /// Sets the value with the registered key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void SetValue(string key, object value);
    }
}