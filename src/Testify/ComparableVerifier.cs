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
    public class ComparableVerifier<T> : ComparisonVerifierBase<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {
        private Func<T, T, bool> opGreaterThan;
        private MethodInfo opGreaterThanMethod;
        private Func<T, T, bool> opGreaterThanOrEqual;
        private MethodInfo opGreaterThanOrEqualMethod;
        private Func<T, T, bool> opLessThan;
        private Func<T, T, bool> opLessThanOrEqual;

        /// <summary>
        /// Gets or sets the factory method used to create unique items used to verify the contract.
        /// </summary>
        /// <value>
        /// The unique items factory.
        /// </value>
        public Func<T[]> OrderedItemsFactory
        {
            get { return this.ItemsFactory; }
            set { this.ItemsFactory = value; }
        }

        /// <inheritdoc/>
        protected override string ItemsFactoryPropertyName => nameof(ComparableVerifier<T>.OrderedItemsFactory);

        /// <inheritdoc/>
        protected override void GetMethods()
        {
            base.GetMethods();
            var type = typeof(T);
            this.opGreaterThanMethod = type.GetRuntimeMethod("op_GreaterThan", new[] { typeof(T), typeof(T) });
            this.opGreaterThan = (Func<T, T, bool>)this.opGreaterThanMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opLessThanMethod = type.GetRuntimeMethod("op_LessThan", new[] { typeof(T), typeof(T) });
            this.opLessThan = (Func<T, T, bool>)opLessThanMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            this.opGreaterThanOrEqualMethod = type.GetRuntimeMethod("op_GreaterThanOrEqual", new[] { typeof(T), typeof(T) });
            this.opGreaterThanOrEqual = (Func<T, T, bool>)this.opGreaterThanOrEqualMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opLessThanOrEqualMethod = type.GetRuntimeMethod("op_LessThanOrEqual", new[] { typeof(T), typeof(T) });
            this.opLessThanOrEqual = (Func<T, T, bool>)opLessThanOrEqualMethod?.CreateDelegate(typeof(Func<T, T, bool>));
        }

        /// <inheritdoc/>
        protected override IEnumerable<Action> GetTests()
        {
            foreach (var test in base.GetTests())
            {
                yield return test;
            }

            yield return this.VerifyComparisonOperatorsDefined;
            yield return this.VerifyCompareToObjWithLesserItems;
            yield return this.VerifyCompareToObjWithEqualItems;
            yield return this.VerifyCompareToObjWithGreaterItems;
            yield return this.VerifyCompareToOtherWithLesserItems;
            yield return this.VerifyCompareToOtherWithEqualItems;
            yield return this.VerifyCompareToOtherWithGreaterItems;
            yield return this.VerifyOpGreaterThanWithLesserItems;
            yield return this.VerifyOpGreaterThanWithEqualItems;
            yield return this.VerifyOpGreaterThanWithGreaterItems;
            yield return this.VerifyOpGreaterThanOrEqualWithLesserItems;
            yield return this.VerifyOpGreaterThanOrEqualWithEqualItems;
            yield return this.VerifyOpGreaterThanOrEqualWithGreaterItems;
            yield return this.VerifyOpLessThanWithLesserItems;
            yield return this.VerifyOpLessThanWithEqualItems;
            yield return this.VerifyOpLessThanWithGreaterItems;
            yield return this.VerifyOpLessThanOrEqualWithLesserItems;
            yield return this.VerifyOpLessThanOrEqualWithEqualItems;
            yield return this.VerifyOpLessThanOrEqualWithGreaterItems;
        }

        private static void SwapLast(T[] first, T[] second)
        {
            Argument.NotNull(first, nameof(first));
            Argument.NotNull(second, nameof(second));

            var temp = first[first.Length - 1];
            first[first.Length - 1] = second[second.Length - 1];
            second[second.Length - 1] = temp;
        }

        private void VerifyCompareToObjWithLesserItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => ((IComparable)x).CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo  failed with values expected to be lesser at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToObjWithEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => ((IComparable)x).CompareTo(y) == 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToObjWithGreaterItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => ((IComparable)x).CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithLesserItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => ((IComparable<T>)x).CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo  failed with values expected to be lesser at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithEqualItems()
        {
            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => ((IComparable<T>)x).CompareTo(y) == 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithGreaterItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => ((IComparable<T>)x).CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyComparisonOperatorsDefined()
        {
            if (this.IsPrimitive || typeof(T) == typeof(string))
            {
                return;
            }

            if (this.opGreaterThanMethod == null || this.opGreaterThanOrEqualMethod == null)
            {
                Fail("Comparison operators must be defined.");
            }
        }

        private void VerifyOpGreaterThanWithLesserItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => this.opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanWithEqualItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => !this.opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanWithGreaterItems()
        {
            if (this.opGreaterThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => !this.opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithLesserItems()
        {
            if (this.opGreaterThanOrEqual == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => this.opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithEqualItems()
        {
            if (this.opGreaterThanOrEqual == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => this.opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithGreaterItems()
        {
            if (this.opGreaterThanOrEqual == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => !this.opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithLesserItems()
        {
            if (this.opLessThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => !this.opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithEqualItems()
        {
            if (this.opLessThan == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => !this.opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithGreaterItems()
        {
            if (this.opLessThan == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => this.opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithLesserItems()
        {
            if (this.opLessThanOrEqual == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(greaterItems, equalItems, (x, y) => !this.opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithEqualItems()
        {
            if (this.opLessThanOrEqual == null)
            {
                return;
            }

            var failure = this.CompareItems(this.BaseItems, this.EqualItems, (x, y) => this.opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithGreaterItems()
        {
            if (this.opLessThanOrEqual == null)
            {
                return;
            }

            var equalItems = this.EqualItems.ToArray();
            var greaterItems = this.NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = this.CompareItems(equalItems, greaterItems, (x, y) => this.opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }
    }
}
