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
            yield return VerifyObjectEqualsIsOverridden;
            yield return VerifyGetHashCodeIsOverridden;
            yield return VerifyEqualityOperatorsDefined;
            yield return VerifyGetHashCodeIsStable;
            yield return VerifyObjectEqualsWithEqualItems;
            yield return VerifyObjectEqualsWithNotEqualItems;
            yield return VerifyEqualsWithEqualItems;
            yield return VerifyEqualsWithNotEqualItems;
            yield return VerifyOpEqualityWithEqualItems;
            yield return VerifyOpEqualityWithNotEqualItems;
            yield return VerifyOpInequalityWithEqualItems;
            yield return VerifyOpInequalityWithNotEqualItems;
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        protected virtual void GetMethods()
        {
            var type = typeof(T);
            objectEqualsMethod = type.GetRuntimeMethod("Equals", new[] { typeof(object) });
            getHashCodeMethod = type.GetRuntimeMethod("GetHashCode", new Type[0]);
            opEqualityMethod = type.GetRuntimeMethod("op_Equality", new[] { typeof(T), typeof(T) });
            opEquality = (Func<T, T, bool>)opEqualityMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opInequalityMethod = type.GetRuntimeMethod("op_Inequality", new[] { typeof(T), typeof(T) });
            opInequality = (Func<T, T, bool>)opInequalityMethod?.CreateDelegate(typeof(Func<T, T, bool>));
        }

        /// <inheritdoc/>
        protected override void VerifyConfiguration()
        {
            base.VerifyConfiguration();

            IsPrimitive = typeof(T).GetTypeInfo().IsPrimitive;

            Assert(ItemsFactory).IsNotNull($"{ItemsFactoryPropertyName} is not set.");

            BaseItems = ItemsFactory();
            Assert(BaseItems).IsNotNull($"{ItemsFactoryPropertyName} did not produce any items.");
            Assert(BaseItems.Length >= 3).IsTrue($"{ItemsFactoryPropertyName} did not produce 3 or more items.");
            Assert(BaseItems).AllItemsAreNotNull($"{ItemsFactoryPropertyName} should not produce null values.");

            EqualItems = ItemsFactory();
            Assert(EqualItems.Length).IsEqualTo(BaseItems.Length, $"{ItemsFactoryPropertyName} is not stable.");
            Assert(EqualItems).AllItemsAreNotNull($"{ItemsFactoryPropertyName} should not produce null values.");

            NotEqualItems = new T[BaseItems.Length];
            Array.Copy(BaseItems, 1, NotEqualItems, 0, BaseItems.Length - 1);
            NotEqualItems[NotEqualItems.Length - 1] = BaseItems[0];

            GetMethods();
        }

        private void VerifyEqualityOperatorsDefined()
        {
            if (IsPrimitive)
            {
                return;
            }

            // Note: This ensures both == and != because defining one without the other results in a CS0216 error.
            if (opEqualityMethod == null)
            {
                Fail("Equality operators must be defined.");
            }
        }

        private void VerifyEqualsWithEqualItems()
        {
            var failure = CompareItems(BaseItems, EqualItems, (x, y) => x.Equals(y));
            if (failure != null)
            {
                Fail($"Equals failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyEqualsWithNotEqualItems()
        {
            var failure = CompareItems(BaseItems, NotEqualItems, (x, y) => !x.Equals(y));
            if (failure != null)
            {
                Fail($"Equals failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyGetHashCodeIsOverridden()
        {
            if (getHashCodeMethod.DeclaringType != typeof(T))
            {
                Fail("Object.GetHashCode must be overridden.");
            }
        }

        private void VerifyGetHashCodeIsStable()
        {
            var hashCodes = BaseItems.Select(i => i.GetHashCode());
            if (!hashCodes.SequenceEqual(hashCodes))
            {
                Fail("GetHashCode is not stable.");
            }
        }

        private void VerifyObjectEqualsIsOverridden()
        {
            if (objectEqualsMethod.DeclaringType != typeof(T))
            {
                Fail("Object.Equals must be overridden.");
            }
        }

        private void VerifyObjectEqualsWithEqualItems()
        {
            var failure = CompareItems(BaseItems, EqualItems, (x, y) => ((object)x).Equals(y));
            if (failure != null)
            {
                Fail($"Object.Equals failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyObjectEqualsWithNotEqualItems()
        {
            var failure = CompareItems(BaseItems, NotEqualItems, (x, y) => !((object)x).Equals(y));
            if (failure != null)
            {
                Fail($"Object.Equals failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpEqualityWithEqualItems()
        {
            if (opEquality == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, opEquality);
            if (failure != null)
            {
                Fail($"op_Equality failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpEqualityWithNotEqualItems()
        {
            if (opEquality == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, NotEqualItems, (x, y) => !opEquality(x, y));
            if (failure != null)
            {
                Fail($"op_Equality failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpInequalityWithEqualItems()
        {
            if (opInequality == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, (x, y) => !opInequality(x, y));
            if (failure != null)
            {
                Fail($"op_Inequality failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpInequalityWithNotEqualItems()
        {
            if (opInequality == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, NotEqualItems, opInequality);
            if (failure != null)
            {
                Fail($"op_Inequality failed with values expected to not be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }
    }
}