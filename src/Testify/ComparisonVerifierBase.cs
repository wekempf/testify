using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Base class for <see cref="EquatableVerifier{T}"/> and <see cref="ComparableVerifier{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type to verify.</typeparam>
    public abstract class ComparisonVerifierBase<T> : ContractVerifier
        where T : IEquatable<T>
    {
        private MethodInfo getHashCodeMethod;
        private MethodInfo objectEqualsMethod;
        private Func<T, T, bool> opEquality;
        private MethodInfo opEqualityMethod;
        private Func<T, T, bool> opInequality;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonVerifierBase{T}"/> class.
        /// </summary>
        protected internal ComparisonVerifierBase()
        {
        }

        /// <summary>
        /// Gets or sets the base items.
        /// </summary>
        /// <value>
        /// The base items.
        /// </value>
        protected T[] BaseItems { get; set; }

        /// <summary>
        /// Gets or sets the equal items.
        /// </summary>
        /// <value>
        /// The equal items.
        /// </value>
        protected T[] EqualItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primitive.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance is primitive; otherwise, <see langword="false" />.
        /// </value>
        protected bool IsPrimitive { get; set; }

        /// <summary>
        /// Gets or sets the factory method used to create items used to verify the contract.
        /// </summary>
        /// <value>
        /// The items factory.
        /// </value>
        protected Func<T[]> ItemsFactory { get; set; }

        /// <summary>
        /// Gets the name of the items factory property.
        /// </summary>
        /// <value>
        /// The name of the items factory property.
        /// </value>
        protected abstract string ItemsFactoryPropertyName { get; }

        /// <summary>
        /// Gets or sets the not equal items.
        /// </summary>
        /// <value>
        /// The not equal items.
        /// </value>
        protected T[] NotEqualItems { get; set; }

        /// <summary>
        /// Compares the items.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>Information about the first comparison failure.</returns>
        protected Tuple<int, T, T> CompareItems(IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer) =>
            first.Zip(second, (f, s) => new { f, s })
                .Select((i, n) => new { n, i.f, i.s })
                .Where(x => !comparer(x.f, x.s))
                .Select(x => Tuple.Create(x.n, x.f, x.s))
                .FirstOrDefault();

        /// <summary>
        /// Gets the test actions to run.
        /// </summary>
        /// <returns>The test actions.</returns>
        protected override IEnumerable<Action> GetTests()
        {
            yield return this.VerifyObjectEqualsIsOverridden;
            yield return this.VerifyGetHashCodeIsOverridden;
            yield return this.VerifyEqualityOperatorsDefined;
            yield return this.VerifyGetHashCodeIsStable;
            yield return this.VerifyObjectEqualsWithEqualItems;
            yield return this.VerifyObjectEqualsWithNotEqualItems;
            yield return this.VerifyEqualsWithEqualItems;
            yield return this.VerifyEqualsWithNotEqualItems;
            yield return this.VerifyOpEqualityWithEqualItems;
            yield return this.VerifyOpEqualityWithNotEqualItems;
            yield return this.VerifyOpInequalityWithEqualItems;
            yield return this.VerifyOpInequalityWithNotEqualItems;
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        protected virtual void GetMethods()
        {
            var type = typeof(T);
            this.objectEqualsMethod = type.GetRuntimeMethod("Equals", new[] { typeof(object) });
            this.getHashCodeMethod = type.GetRuntimeMethod("GetHashCode", new Type[0]);
            this.opEqualityMethod = type.GetRuntimeMethod("op_Equality", new[] { typeof(T), typeof(T) });
            this.opEquality = (Func<T, T, bool>)this.opEqualityMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opInequalityMethod = type.GetRuntimeMethod("op_Inequality", new[] { typeof(T), typeof(T) });
            this.opInequality = (Func<T, T, bool>)opInequalityMethod?.CreateDelegate(typeof(Func<T, T, bool>));
        }

        /// <inheritdoc/>
        protected override void VerifyConfiguration()
        {
            base.VerifyConfiguration();

            this.IsPrimitive = typeof(T).GetTypeInfo().IsPrimitive;

            Assert(this.ItemsFactory).IsNotNull($"{this.ItemsFactoryPropertyName} is not set.");

            this.BaseItems = this.ItemsFactory();
            Assert(this.BaseItems).IsNotNull($"{this.ItemsFactoryPropertyName} did not produce any items.");
            Assert(this.BaseItems.Length >= 3).IsTrue($"{this.ItemsFactoryPropertyName} did not produce 3 or more items.");
            Assert(this.BaseItems).AllItemsAreNotNull($"{this.ItemsFactoryPropertyName} should not produce null values.");

            this.EqualItems = this.ItemsFactory();
            Assert(this.EqualItems.Length).IsEqualTo(this.BaseItems.Length, $"{this.ItemsFactoryPropertyName} is not stable.");
            Assert(this.EqualItems).AllItemsAreNotNull($"{this.ItemsFactoryPropertyName} should not produce null values.");

            this.NotEqualItems = new T[this.BaseItems.Length];
            Array.Copy(this.BaseItems, 1, this.NotEqualItems, 0, this.BaseItems.Length - 1);
            this.NotEqualItems[this.NotEqualItems.Length - 1] = this.BaseItems[0];

            this.GetMethods();
        }

        private void VerifyEqualityOperatorsDefined()
        {
            if (this.IsPrimitive)
            {
                return;
            }

            // Note: This ensures both == and != because defining one without the other results in a CS0216 error.
            if (this.opEqualityMethod == null)
            {
                Fail("Equality operators must be defined.");
            }
        }

        private void VerifyEqualsWithEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => x.Equals(y));
            if (failure != null)
            {
                Fail($"Equals failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyEqualsWithNotEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.NotEqualItems, (x, y) => !x.Equals(y));
            if (failure != null)
            {
                Fail($"Equals failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyGetHashCodeIsOverridden()
        {
            if (this.getHashCodeMethod.DeclaringType != typeof(T))
            {
                Fail("Object.GetHashCode must be overridden.");
            }
        }

        private void VerifyGetHashCodeIsStable()
        {
            var hashCodes = this.BaseItems.Select(i => i.GetHashCode());
            if (!hashCodes.SequenceEqual(hashCodes))
            {
                Fail("GetHashCode is not stable.");
            }
        }

        private void VerifyObjectEqualsIsOverridden()
        {
            if (this.objectEqualsMethod.DeclaringType != typeof(T))
            {
                Fail("Object.Equals must be overridden.");
            }
        }

        private void VerifyObjectEqualsWithEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => ((object)x).Equals(y));
            if (failure != null)
            {
                Fail($"Object.Equals failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyObjectEqualsWithNotEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.NotEqualItems, (x, y) => !((object)x).Equals(y));
            if (failure != null)
            {
                Fail($"Object.Equals failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpEqualityWithEqualItems()
        {
            if (this.opEquality == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, this.opEquality);
            if (failure != null)
            {
                Fail($"op_Equality failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpEqualityWithNotEqualItems()
        {
            if (this.opEquality == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.NotEqualItems, (x, y) => !this.opEquality(x, y));
            if (failure != null)
            {
                Fail($"op_Equality failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpInequalityWithEqualItems()
        {
            if (this.opInequality == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => !this.opInequality(x, y));
            if (failure != null)
            {
                Fail($"op_Inequality failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpInequalityWithNotEqualItems()
        {
            if (this.opInequality == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.NotEqualItems, this.opInequality);
            if (failure != null)
            {
                Fail($"op_Inequality failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }
    }
}