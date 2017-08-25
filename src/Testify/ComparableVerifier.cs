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
            get => ItemsFactory;
            set => ItemsFactory = value;
        }

        /// <inheritdoc/>
        protected override string ItemsFactoryPropertyName => nameof(ComparableVerifier<T>.OrderedItemsFactory);

        /// <inheritdoc/>
        protected override void GetMethods()
        {
            base.GetMethods();
            var type = typeof(T);
            opGreaterThanMethod = type.GetRuntimeMethod("op_GreaterThan", new[] { typeof(T), typeof(T) });
            opGreaterThan = (Func<T, T, bool>)opGreaterThanMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opLessThanMethod = type.GetRuntimeMethod("op_LessThan", new[] { typeof(T), typeof(T) });
            opLessThan = (Func<T, T, bool>)opLessThanMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            opGreaterThanOrEqualMethod = type.GetRuntimeMethod("op_GreaterThanOrEqual", new[] { typeof(T), typeof(T) });
            opGreaterThanOrEqual = (Func<T, T, bool>)opGreaterThanOrEqualMethod?.CreateDelegate(typeof(Func<T, T, bool>));
            var opLessThanOrEqualMethod = type.GetRuntimeMethod("op_LessThanOrEqual", new[] { typeof(T), typeof(T) });
            opLessThanOrEqual = (Func<T, T, bool>)opLessThanOrEqualMethod?.CreateDelegate(typeof(Func<T, T, bool>));
        }

        /// <inheritdoc/>
        protected override IEnumerable<Action> GetTests()
        {
            var tests = base.GetTests() as TestCollection;
            tests.AddTest(nameof(VerifyComparisonOperatorsDefined), () => VerifyComparisonOperatorsDefined());
            tests.AddTest(nameof(VerifyCompareToObjWithLesserItems), () => VerifyCompareToObjWithLesserItems());
            tests.AddTest(nameof(VerifyCompareToObjWithEqualItems), () => VerifyCompareToObjWithEqualItems());
            tests.AddTest(nameof(VerifyCompareToObjWithGreaterItems), () => VerifyCompareToObjWithGreaterItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithLesserItems), () => VerifyCompareToOtherWithLesserItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithEqualItems), () => VerifyCompareToOtherWithEqualItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithGreaterItems), () => VerifyCompareToOtherWithGreaterItems());
            tests.AddTest(nameof(VerifyOpGreaterThanWithLesserItems), () => VerifyOpGreaterThanWithLesserItems());
            tests.AddTest(nameof(VerifyOpGreaterThanWithEqualItems), () => VerifyOpGreaterThanWithEqualItems());
            tests.AddTest(nameof(VerifyOpGreaterThanWithGreaterItems), () => VerifyOpGreaterThanWithGreaterItems());
            tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithLesserItems), () => VerifyOpGreaterThanOrEqualWithLesserItems());
            tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithEqualItems), () => VerifyOpGreaterThanOrEqualWithEqualItems());
            tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithGreaterItems), () => VerifyOpGreaterThanOrEqualWithGreaterItems());
            tests.AddTest(nameof(VerifyOpLessThanWithLesserItems), () => VerifyOpLessThanWithLesserItems());
            tests.AddTest(nameof(VerifyOpLessThanWithEqualItems), () => VerifyOpLessThanWithEqualItems());
            tests.AddTest(nameof(VerifyOpLessThanWithGreaterItems), () => VerifyOpLessThanWithGreaterItems());
            tests.AddTest(nameof(VerifyOpLessThanOrEqualWithLesserItems), () => VerifyOpLessThanOrEqualWithLesserItems());
            tests.AddTest(nameof(VerifyOpLessThanOrEqualWithEqualItems), () => VerifyOpLessThanOrEqualWithEqualItems());
            tests.AddTest(nameof(VerifyOpLessThanOrEqualWithGreaterItems), () => VerifyOpLessThanOrEqualWithGreaterItems());
            return tests;
        }

        private static void SwapLast(T[] first, T[] second)
        {
            Argument.NotNull(first, nameof(first));
            Argument.NotNull(second, nameof(second));

            var temp = first[first.Length - 1];
            first[first.Length - 1] = second[second.Length - 1];
            second[second.Length - 1] = temp;
        }

        private void VerifyCompareToObjWithEqualItems()
        {
            var failure = CompareItems(BaseItems, EqualItems, (x, y) => ((IComparable)x).CompareTo(y) == 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToObjWithGreaterItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => ((IComparable)x).CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToObjWithLesserItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => ((IComparable)x).CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo  failed with values expected to be lesser at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithEqualItems()
        {
            var failure = CompareItems(BaseItems, EqualItems, (x, y) => ((IComparable<T>)x).CompareTo(y) == 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be equal at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithGreaterItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => ((IComparable<T>)x).CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyCompareToOtherWithLesserItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => ((IComparable<T>)x).CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo  failed with values expected to be lesser at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyComparisonOperatorsDefined()
        {
            if (IsPrimitive || typeof(T) == typeof(string))
            {
                return;
            }

            if (opGreaterThanMethod == null || opGreaterThanOrEqualMethod == null)
            {
                Fail("Comparison operators must be defined.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithEqualItems()
        {
            if (opGreaterThanOrEqual == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, (x, y) => opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithGreaterItems()
        {
            if (opGreaterThanOrEqual == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => !opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithLesserItems()
        {
            if (opGreaterThanOrEqual == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => opGreaterThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanWithEqualItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, (x, y) => !opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanWithGreaterItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => !opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpGreaterThanWithLesserItems()
        {
            if (opGreaterThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => opGreaterThan(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithEqualItems()
        {
            if (opLessThanOrEqual == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, (x, y) => opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithGreaterItems()
        {
            if (opLessThanOrEqual == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithLesserItems()
        {
            if (opLessThanOrEqual == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => !opLessThanOrEqual(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithEqualItems()
        {
            if (opLessThan == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, EqualItems, (x, y) => !opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to not be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithGreaterItems()
        {
            if (opLessThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }

        private void VerifyOpLessThanWithLesserItems()
        {
            if (opLessThan == null)
            {
                return;
            }

            var equalItems = EqualItems.ToArray();
            var greaterItems = NotEqualItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => !opLessThan(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Item1}. Expected: <{failure.Item3}>. Actual: <{failure.Item2}>.");
            }
        }
    }
}