using System;
using System.Linq;
using System.Reflection;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Compare tests.
    /// </summary>
    /// <typeparam name="T">Type to compare.</typeparam>
    /// <seealso cref="Testify.EqualityTests{T}" />
    internal class CompareTests<T> : EqualityTests<T>
        where T : IEquatable<T>, IComparable<T>
    {
        private static readonly Func<T, T, bool> OpGreaterThanFunc;
        private static readonly Func<T, T, bool> OpGreaterThanOrEqualFunc;
        private static readonly Func<T, T, bool> OpLessThanFunc;
        private static readonly Func<T, T, bool> OpLessThanOrEqualFunc;

        static CompareTests()
        {
            var typeInfo = typeof(T).GetTypeInfo();
            if (!typeInfo.IsPrimitive)
            {
                var method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_GreaterThan"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpGreaterThanFunc = method?.CreateFunc<T, T, bool>();

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_GreaterThanOrEqual"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpGreaterThanOrEqualFunc = method?.CreateFunc<T, T, bool>();

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_LessThan"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpLessThanFunc = method?.CreateFunc<T, T, bool>();

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_LessThanOrEqual"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpLessThanOrEqualFunc = method?.CreateFunc<T, T, bool>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTests{T}"/> class.
        /// </summary>
        /// <param name="itemsFactory">The items factory.</param>
        public CompareTests(Func<T[]> itemsFactory)
            : base(itemsFactory)
        {
        }

        /// <inheritdoc/>
        internal override TestCollection GetTests()
        {
            var isValueType = typeof(T).GetTypeInfo().IsValueType;
            var tests = base.GetTests();
            tests.AddTest(nameof(VerifyCompareToObjWithNull), () => VerifyCompareToObjWithNull());
            tests.AddTest(nameof(VerifyCompareToObjWithLesserItems), () => VerifyCompareToObjWithLesserItems());
            tests.AddTest(nameof(VerifyCompareToObjWithEqualItems), () => VerifyCompareToObjWithEqualItems());
            tests.AddTest(nameof(VerifyCompareToObjWithGreaterItems), () => VerifyCompareToObjWithGreaterItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithLesserItems), () => VerifyCompareToOtherWithLesserItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithItems), () => VerifyCompareToOtherWithItems());
            tests.AddTest(nameof(VerifyCompareToOtherWithGreaterItems), () => VerifyCompareToOtherWithGreaterItems());
            if (!isValueType)
            {
                tests.AddTest(nameof(VerifyCompareToOtherWithNull), () => VerifyCompareToOtherWithNull());
            }

            if (!SkipOperatorTests)
            {
                tests.AddTest(nameof(VerifyComparisonOperatorsDefined), () => VerifyComparisonOperatorsDefined());
                tests.AddTest(nameof(VerifyOpGreaterThanWithLesserItems), () => VerifyOpGreaterThanWithLesserItems());
                tests.AddTest(nameof(VerifyOpGreaterThanWithItems), () => VerifyOpGreaterThanWithItems());
                tests.AddTest(nameof(VerifyOpGreaterThanWithGreaterItems), () => VerifyOpGreaterThanWithGreaterItems());
                tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithLesserItems), () => VerifyOpGreaterThanOrEqualWithLesserItems());
                tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithItems), () => VerifyOpGreaterThanOrEqualWithItems());
                tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithGreaterItems), () => VerifyOpGreaterThanOrEqualWithGreaterItems());
                tests.AddTest(nameof(VerifyOpLessThanWithLesserItems), () => VerifyOpLessThanWithLesserItems());
                tests.AddTest(nameof(VerifyOpLessThanWithItems), () => VerifyOpLessThanWithItems());
                tests.AddTest(nameof(VerifyOpLessThanWithGreaterItems), () => VerifyOpLessThanWithGreaterItems());
                tests.AddTest(nameof(VerifyOpLessThanOrEqualWithLesserItems), () => VerifyOpLessThanOrEqualWithLesserItems());
                tests.AddTest(nameof(VerifyOpLessThanOrEqualWithItems), () => VerifyOpLessThanOrEqualWithItems());
                tests.AddTest(nameof(VerifyOpLessThanOrEqualWithGreaterItems), () => VerifyOpLessThanOrEqualWithGreaterItems());
                if (!isValueType)
                {
                    tests.AddTest(nameof(VerifyOpGreaterThanWithLeftNull), () => VerifyOpGreaterThanWithLeftNull());
                    tests.AddTest(nameof(VerifyOpGreaterThanWithRightNull), () => VerifyOpGreaterThanWithRightNull());
                    tests.AddTest(nameof(VerifyOpGreaterThanWithRightAndLeftNull), () => VerifyOpGreaterThanWithRightAndLeftNull());
                    tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithLeftNull), () => VerifyOpGreaterThanOrEqualWithLeftNull());
                    tests.AddTest(nameof(VerifyOpGreaterThanOrEqualWithRightNull), () => VerifyOpGreaterThanOrEqualWithRightNull());
                    tests.AddTest(
                        nameof(VerifyOpGreaterThanOrEqualWithRightAndLeftNull),
                        () => VerifyOpGreaterThanOrEqualWithRightAndLeftNull());
                    tests.AddTest(nameof(VerifyOpLessThanWithLeftNull), () => VerifyOpLessThanWithLeftNull());
                    tests.AddTest(nameof(VerifyOpLessThanWithRightNull), () => VerifyOpLessThanWithRightNull());
                    tests.AddTest(nameof(VerifyOpLessThanWithRightAndLeftNull), () => VerifyOpLessThanWithRightAndLeftNull());
                    tests.AddTest(nameof(VerifyOpLessThanOrEqualWithLeftNull), () => VerifyOpLessThanOrEqualWithLeftNull());
                    tests.AddTest(nameof(VerifyOpLessThanOrEqualWithRightNull), () => VerifyOpLessThanOrEqualWithRightNull());
                    tests.AddTest(
                        nameof(VerifyOpLessThanOrEqualWithRightAndLeftNull),
                        () => VerifyOpLessThanOrEqualWithRightAndLeftNull());
                }
            }

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

        private static void VerifyComparisonOperatorsDefined()
        {
            var type = typeof(T);
            if (type.GetTypeInfo().IsPrimitive || type == typeof(string))
            {
                return;
            }

            if (OpGreaterThanFunc == null
                || OpGreaterThanOrEqualFunc == null
                || OpLessThanFunc == null
                || OpLessThanOrEqualFunc == null)
            {
                Fail("Comparison operators must be defined.");
            }
        }

        private void VerifyCompareToObjWithEqualItems()
        {
            var result = CompareItems(BaseItems, Items, (x, y) => ((IComparable)x).CompareTo(y) == 0);
            if (result != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyCompareToObjWithGreaterItems()
        {
            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => ((IComparable)x).CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyCompareToObjWithLesserItems()
        {
            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => ((IComparable)x).CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo  failed with values expected to be lesser at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyCompareToObjWithNull()
        {
            var failure = CompareItems(Items, Items, (x, _) => ((IComparable)x).CompareTo(null) > 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyCompareToOtherWithGreaterItems()
        {
            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => x.CompareTo(y) < 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyCompareToOtherWithItems()
        {
            var failure = CompareItems(BaseItems, Items, (x, y) => x.CompareTo(y) == 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo failed with values expected to be equal at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyCompareToOtherWithLesserItems()
        {
            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => x.CompareTo(y) > 0);
            if (failure != null)
            {
                Fail($"IComparable<T>.CompareTo  failed with values expected to be lesser at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyCompareToOtherWithNull()
        {
            var failure = CompareItems(Items, Items, (x, _) => x.CompareTo(default(T)) > 0);
            if (failure != null)
            {
                Fail($"IComparable.CompareTo failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithGreaterItems()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => !OpGreaterThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithItems()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, Items, (x, y) => OpGreaterThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to not be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithLeftNull()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => !OpGreaterThanOrEqualFunc(default(T), x));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithLesserItems()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => OpGreaterThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithRightAndLeftNull()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

#pragma warning disable RCS1163 // Unused parameter.
            var failure = CompareItems(Items, Items, (x, y) => OpGreaterThanOrEqualFunc(default(T), default(T)));
#pragma warning restore RCS1163 // Unused parameter.

            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanOrEqualWithRightNull()
        {
            if (OpGreaterThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => OpGreaterThanOrEqualFunc(x, default(T)));
            if (failure != null)
            {
                Fail($"op_GreaterThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanWithGreaterItems()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => !OpGreaterThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanWithItems()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, Items, (x, y) => !OpGreaterThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to not be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanWithLeftNull()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => !OpGreaterThanFunc(default(T), x));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanWithLesserItems()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => OpGreaterThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_GreaterThan failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpGreaterThanWithRightAndLeftNull()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

#pragma warning disable RCS1163 // Unused parameter.
            var failure = CompareItems(Items, Items, (x, y) => !OpGreaterThanFunc(default(T), default(T)));
#pragma warning restore RCS1163 // Unused parameter.

            if (failure != null)
            {
                Fail($"op_GreaterThan failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpGreaterThanWithRightNull()
        {
            if (OpGreaterThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => OpGreaterThanFunc(x, default(T)));

            if (failure != null)
            {
                Fail($"op_GreaterThan failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanOrEqualWithGreaterItems()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => OpLessThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithItems()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, Items, (x, y) => OpLessThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to not be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithLeftNull()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => OpLessThanOrEqualFunc(default(T), x));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanOrEqualWithLesserItems()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => !OpLessThanOrEqualFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanOrEqualWithRightAndLeftNull()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

#pragma warning disable RCS1163 // Unused parameter.
            var failure = CompareItems(Items, Items, (x, y) => OpLessThanOrEqualFunc(default(T), default(T)));
#pragma warning restore RCS1163 // Unused parameter.

            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanOrEqualWithRightNull()
        {
            if (OpLessThanOrEqualFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => !OpLessThanOrEqualFunc(x, default(T)));
            if (failure != null)
            {
                Fail($"op_LessThanOrEqual failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanWithGreaterItems()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(equalItems, greaterItems, (x, y) => OpLessThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanWithItems()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(BaseItems, Items, (x, y) => !OpLessThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to not be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanWithLeftNull()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => OpLessThanFunc(default(T), x));
            if (failure != null)
            {
                Fail($"op_LessThan failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanWithLesserItems()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

            var equalItems = Items.ToArray();
            var greaterItems = ShiftedItems.ToArray();
            SwapLast(equalItems, greaterItems);
            var failure = CompareItems(greaterItems, equalItems, (x, y) => !OpLessThanFunc(x, y));
            if (failure != null)
            {
                Fail($"op_LessThan failed with values expected to be greater at index {failure.Value.Index}. "
                    + $"Expected: <{failure.Value.Right}>. Actual: <{failure.Value.Left}>.");
            }
        }

        private void VerifyOpLessThanWithRightAndLeftNull()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

#pragma warning disable RCS1163 // Unused parameter.
            var failure = CompareItems(Items, Items, (x, y) => !OpLessThanFunc(default(T), default(T)));
#pragma warning restore RCS1163 // Unused parameter.

            if (failure != null)
            {
                Fail($"op_LessThan failed with null value at index {failure.Value.Index}.");
            }
        }

        private void VerifyOpLessThanWithRightNull()
        {
            if (OpLessThanFunc == null)
            {
                return;
            }

            var failure = CompareItems(Items, Items, (x, _) => !OpLessThanFunc(x, default(T)));
            if (failure != null)
            {
                Fail($"op_LessThan failed with null value at index {failure.Value.Index}.");
            }
        }
    }
}