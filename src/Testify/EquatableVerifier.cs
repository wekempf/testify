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
        public bool SkipOperatorTests { get; set; }

        /// <summary>
        /// Gets or sets the factory method used to create unique items used to verify
        /// the contract.
        /// </summary>
        /// <value>
        /// The unique items factory.
        /// </value>
        public Func<T[]> UniqueItemsFactory { get; set; }

        private EqualityTests<T> EqualityTests { get; set; }

        protected override IEnumerable<Action> GetTests()
        {
            return EqualityTests.GetTests();
        }

        protected override void VerifyConfiguration()
        {
            EqualityTests = new EqualityTests<T>(UniqueItemsFactory)
            {
                SkipOperatorTests = SkipOperatorTests
            };
            EqualityTests.VerifyConfiguration(nameof(UniqueItemsFactory));
        }
    }
}