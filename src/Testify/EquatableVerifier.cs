using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Verifies the specified type conforms to the <see cref="IEquatable{T}"/> contract.
    /// </summary>
    /// <typeparam name="T">The type to verify.</typeparam>
    public class EquatableVerifier<T> : ComparisonVerifierBase<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Gets or sets the factory method used to create unique items used to verify
        /// the contract.
        /// </summary>
        /// <value>
        /// The unique items factory.
        /// </value>
        public Func<T[]> UniqueItemsFactory
        {
            get { return ItemsFactory; }
            set { ItemsFactory = value; }
        }

        /// <inheritdoc/>
        protected override string ItemsFactoryPropertyName => nameof(EquatableVerifier<T>.UniqueItemsFactory);
    }
}