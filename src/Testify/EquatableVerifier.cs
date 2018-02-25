using System;
using System.Collections.Generic;

namespace Testify
{
    /// <summary>
    /// Verifies the specified type conforms to the <see cref="IEquatable{T}"/> contract.
    /// </summary>
    /// <typeparam name="T">The type to verify.</typeparam>
    public class EquatableVerifier<T> : ContractVerifier
        where T : IEquatable<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether operator tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if operator tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        public bool SkipOperatorTests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether immutability tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if immutability tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        public bool SkipImmutabilityTests { get; set; }

        /// <summary>
        /// Gets or sets the factory method used to create unique items used to verify
        /// the contract.
        /// </summary>
        /// <value>
        /// The unique items factory.
        /// </value>
        public Func<T[]> UniqueItemsFactory { get; set; }

        private EqualityTests<T> EqualityTests { get; set; }

        /// <inheritdoc/>
        protected override IEnumerable<Action> GetTests()
        {
            return EqualityTests.GetTests();
        }

        /// <inheritdoc/>
        protected override void VerifyConfiguration()
        {
            EqualityTests = new EqualityTests<T>(UniqueItemsFactory)
            {
                SkipImmutabilityTests = SkipImmutabilityTests,
                SkipOperatorTests = SkipOperatorTests,
            };
            EqualityTests.VerifyConfiguration(nameof(UniqueItemsFactory));
        }
    }
}