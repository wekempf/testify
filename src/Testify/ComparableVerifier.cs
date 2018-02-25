using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Verifies the specified type conforms to the <see cref="IComparable{T}"/> contract.
    /// </summary>
    /// <typeparam name="T">The type to verify.</typeparam>
    public class ComparableVerifier<T> : ContractVerifier
        where T : IEquatable<T>, IComparable<T>, IComparable
    {
        /// <summary>
        /// Gets or sets the factory method used to create unique items used to verify the contract.
        /// </summary>
        /// <value>
        /// The unique items factory.
        /// </value>
        public Func<T[]> OrderedItemsFactory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether immutability tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if immutability tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        public bool SkipImmutabilityTests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether operator tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if operator tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        public bool SkipOperatorTests { get; set; }

        private CompareTests<T> CompareTests { get; set; }

        /// <inheritdoc/>
        protected override IEnumerable<Action> GetTests()
        {
            return CompareTests.GetTests();
        }

        /// <inheritdoc/>
        protected override void VerifyConfiguration()
        {
            CompareTests = new CompareTests<T>(OrderedItemsFactory)
            {
                SkipImmutabilityTests = SkipImmutabilityTests,
                SkipOperatorTests = SkipOperatorTests,
            };
            CompareTests.VerifyConfiguration(nameof(OrderedItemsFactory));
        }
    }
}