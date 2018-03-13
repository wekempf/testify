using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Equality tests.
    /// </summary>
    /// <typeparam name="T">The type to test.</typeparam>
    internal class EqualityTests<T>
            where T : IEquatable<T>
    {
        private static readonly MethodInfo GetHashCodeFunc;
        private static readonly MethodInfo ObjectEqualsFunc;
        private static readonly Func<T, T, bool> OpEqualityFunc;
        private static readonly Func<T, T, bool> OpInequalityFunc;

        static EqualityTests()
        {
            var typeInfo = typeof(T).GetTypeInfo();
            if (!typeInfo.IsPrimitive)
            {
                var method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == nameof(GetHashCode)
                        && !m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new Type[0]))
                    .OnlyOrDefault();
                GetHashCodeFunc = method;

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == nameof(Equals)
                        && !m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(object) }))
                    .OnlyOrDefault();
                ObjectEqualsFunc = method;

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_Equality"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpEqualityFunc = method?.CreateFunc<T, T, bool>();

                method = typeInfo.DeclaredMethods
                    .Where(m => m.Name == "op_Inequality"
                        && m.IsStatic
                        && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(T), typeof(T) }))
                    .OnlyOrDefault();
                OpInequalityFunc = method?.CreateFunc<T, T, bool>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityTests{T}"/> class.
        /// </summary>
        /// <param name="itemsFactory">The items factory.</param>
        internal EqualityTests(Func<T[]> itemsFactory)
        {
            ItemsFactory = itemsFactory;
            if (ItemsFactory != null)
            {
                BaseItems = itemsFactory.Invoke();
                if (BaseItems != null)
                {
                    Items = itemsFactory.Invoke();
                    ShiftedItems = new T[Items.Length];
                    Array.Copy(Items, 1, ShiftedItems, 0, Items.Length - 1);
                    ShiftedItems[Items.Length - 1] = Items[0];
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether immutability tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if immutability tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        internal bool SkipImmutabilityTests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether operator tests should be skipped.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if operator tests should be skipped; otherwise, <see langword="false"/>.
        /// </value>
        internal bool SkipOperatorTests { get; set; }

        /// <summary>
        /// Gets the base items.
        /// </summary>
        /// <value>
        /// The base items.
        /// </value>
        protected internal T[] BaseItems { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        protected internal T[] Items { get; }

        /// <summary>
        /// Gets or sets the items factory.
        /// </summary>
        /// <value>
        /// The items factory.
        /// </value>
        protected internal Func<T[]> ItemsFactory { get; set; }

        /// <summary>
        /// Gets the shifted items.
        /// </summary>
        /// <value>
        /// The shifted items.
        /// </value>
        protected internal T[] ShiftedItems { get; }

        /// <summary>
        /// Gets the tests.
        /// </summary>
        /// <returns>The tests.</returns>
        internal virtual TestCollection GetTests()
        {
            var isValueType = typeof(T).GetTypeInfo().IsValueType;
            var tests = new TestCollection();
            tests.AddTest(nameof(VerifyObjectEqualsIsOverridden), () => VerifyObjectEqualsIsOverridden());
            tests.AddTest(nameof(VerifyObjectEqualsWithEqualItems), () => VerifyObjectEqualsWithEqualItems());
            tests.AddTest(nameof(VerifyObjectEqualsWithNull), () => VerifyObjectEqualsWithNull());
            tests.AddTest(nameof(VerifyGetHashCodeIsOverridden), () => VerifyGetHashCodeIsOverridden());
            tests.AddTest(nameof(VerifyGetHashCodeIsStable), () => VerifyGetHashCodeIsStable());
            tests.AddTest(nameof(VerifyGetHashCodeWithEqualItems), () => VerifyGetHashCodeWithEqualItems());
            tests.AddTest(nameof(VerifyObjectEqualsWithInequalItems), () => VerifyObjectEqualsWithInequalItems());
            tests.AddTest(nameof(VerifyEqualsWithEqualItems), () => VerifyEqualsWithEqualItems());
            tests.AddTest(nameof(VerifyEqualsWithInequalItems), () => VerifyEqualsWithInequalItems());
            if (!isValueType)
            {
                tests.AddTest(nameof(VerifyEqualsWithNull), () => VerifyEqualsWithNull());
            }

            if (!SkipOperatorTests)
            {
                tests.AddTest(nameof(VerifyEqualityOperatorsDefined), () => VerifyEqualityOperatorsDefined());
                tests.AddTest(nameof(VerifyOpEqualityWithEqualItems), () => VerifyOpEqualityWithEqualItems());
                tests.AddTest(nameof(VerifyOpEqualityWithNotEqualItems), () => VerifyOpEqualityWithNotEqualItems());
                tests.AddTest(nameof(VerifyOpInequalityWithEqualItems), () => VerifyOpInequalityWithEqualItems());
                tests.AddTest(nameof(VerifyOpInequalityWithNotEqualItems), () => VerifyOpInequalityWithNotEqualItems());
                if (!isValueType)
                {
                    tests.AddTest(nameof(VerifyOpEqualityWithLeftNull), () => VerifyOpEqualityWithLeftNull());
                    tests.AddTest(nameof(VerifyOpEqualityWithRightNull), () => VerifyOpEqualityWithRightNull());
                    tests.AddTest(nameof(VerifyOpEqualityWithRightAndLeftNull), () => VerifyOpEqualityWithRightAndLeftNull());
                    tests.AddTest(nameof(VerifyOpInequalityWithLeftNull), () => VerifyOpInequalityWithLeftNull());
                    tests.AddTest(nameof(VerifyOpInequalityWithRightNull), () => VerifyOpInequalityWithRightNull());
                    tests.AddTest(nameof(VerifyOpInequalityWithRightAndLeftNull), () => VerifyOpInequalityWithRightAndLeftNull());
                }
            }

            if (!SkipImmutabilityTests)
            {
                tests.AddTest(nameof(VerifyImmutability), () => VerifyImmutability());
            }

            return tests;
        }

        /// <summary>
        /// Verifies the configuration.
        /// </summary>
        /// <param name="factoryName">Name of the factory.</param>
        internal void VerifyConfiguration(string factoryName)
        {
            Assert(ItemsFactory).IsNotNull($"{factoryName} is not set.");
            Assert(BaseItems).IsNotNull($"{factoryName} did not produce any items.");
            Assert(BaseItems?.Length >= 3).IsTrue($"{factoryName} did not produce 3 or more items.");
            Assert(BaseItems).AllItemsAreNotNull($"{factoryName} should not produce null values.");
            Assert(Items.Length).IsEqualTo(BaseItems.Length, $"{factoryName} is not stable.");
        }

        /// <summary>
        /// Compares the items.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>The result.</returns>
        protected internal static (int Index, T Left, T Right)? CompareItems(
                    IEnumerable<T> first,
                    IEnumerable<T> second,
                    Func<T, T, bool> comparer) =>
                    first.Zip(second, (f, s) => new { f, s })
                        .Select((i, n) => new { n, i.f, i.s })
                        .Where(x => !comparer(x.f, x.s))
                        .Select(x => (x.n, x.f, x.s).AsNullable())
                        .FirstOrDefault();

        private static void VerifyEqualityOperatorsDefined()
        {
            var typeInfo = typeof(T).GetTypeInfo();
            if (typeInfo.IsPrimitive)
            {
                return;
            }

            if (OpEqualityFunc == null || OpInequalityFunc == null)
            {
                Fail("Equality operators must be defined.");
            }
        }

        private static void VerifyGetHashCodeIsOverridden()
        {
            if (typeof(T).GetTypeInfo().IsPrimitive)
            {
                return;
            }

            if (GetHashCodeFunc == null)
            {
                Fail("Object.GetHashCode must be overridden.");
            }
        }

        private static void VerifyObjectEqualsIsOverridden()
        {
            if (typeof(T).GetTypeInfo().IsPrimitive)
            {
                return;
            }

            if (ObjectEqualsFunc == null)
            {
                Fail("Object.Equals must be overridden.");
            }
        }

        private static void VerifyOpEqualityWithRightAndLeftNull()
        {
            if (OpEqualityFunc == null)
            {
                return;
            }

            if (!OpEqualityFunc(default(T), default(T)))
            {
                Fail($"op_Equality failed when both right hand side and left hand side are null.");
            }
        }

        private static void VerifyOpInequalityWithRightAndLeftNull()
        {
            if (OpInequalityFunc == null)
            {
                return;
            }

            if (OpInequalityFunc(default(T), default(T)))
            {
                Fail($"op_Inequality failed when both right hand side and left hand side are null.");
            }
        }

        private void VerifyEqualsWithEqualItems()
        {
            var result = CompareItems(BaseItems, Items, (x, y) => x.Equals(y));
            if (result != null)
            {
                Fail($"IEquatable<{typeof(T).Name}>.Equals failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyEqualsWithInequalItems()
        {
            var result = CompareItems(BaseItems, ShiftedItems, (x, y) => !x.Equals(y));
            if (result != null)
            {
                Fail($"IEquatable<{typeof(T).Name}>.Equals failed with values expected to not be equal at index "
                    + $"{result.Value.Index}. Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyEqualsWithNull()
        {
            var result = CompareItems(BaseItems, BaseItems, (x, _) => !x.Equals(default(T)));
            if (result != null)
            {
                Fail($"IEquatable<{typeof(T).Name}>.Equals failed with null at index {result.Value.Index}.");
            }
        }

        private void VerifyGetHashCodeIsStable()
        {
            var hashCodes = Items.Select(i => i.GetHashCode());
            if (!hashCodes.SequenceEqual(Items.Select(i => i.GetHashCode())))
            {
                Fail("GetHashCode is not stable.");
            }
        }

        private void VerifyGetHashCodeWithEqualItems()
        {
            var result = CompareItems(BaseItems, Items, (x, y) => x.GetHashCode() == y.GetHashCode());
            if (result != null)
            {
                Fail($"Object.GetHashCode failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyImmutability()
        {
            var anon = new AnonymousData();
            var left = BaseItems[0];
            var right = ItemsFactory.Invoke().First();
            var typeInfo = typeof(T).GetTypeInfo();
            foreach (var property in typeInfo.DeclaredProperties.Where(p => p.CanRead && p.CanWrite))
            {
                var curValue = property.GetValue(right);
                var newValue = anon.Any(property.PropertyType, v => curValue == null ? v != null : !curValue.Equals(v));
                property.SetValue(right, newValue);
                if (!left.Equals(right))
                {
                    Fail($"Failed immutability check. Modifying {property.Name} changed equality.");
                }

                if (left.GetHashCode() != right.GetHashCode())
                {
                    Fail($"Failed immutability check. Modifying {property.Name} changed the hash code.");
                }
            }
        }

        private void VerifyObjectEqualsWithEqualItems()
        {
            var result = CompareItems(BaseItems, Items, (x, y) => ((object)x).Equals(y));
            if (result != null)
            {
                Fail($"Object.Equals failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyObjectEqualsWithInequalItems()
        {
            var result = CompareItems(BaseItems, ShiftedItems, (x, y) => !((object)x).Equals(y));
            if (result != null)
            {
                Fail($"Object.Equals failed with values expected to not be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyObjectEqualsWithNull()
        {
            var result = CompareItems(BaseItems, BaseItems, (x, _) => !((object)x).Equals(null));
            if (result != null)
            {
                Fail($"Object.Equals failed with null at index {result.Value.Index}.");
            }
        }

        private void VerifyOpEqualityWithEqualItems()
        {
            if (OpEqualityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, Items, OpEqualityFunc);
            if (result != null)
            {
                Fail($"op_Equality failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyOpEqualityWithLeftNull()
        {
            if (OpEqualityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, BaseItems, (x, _) => !OpEqualityFunc(default(T), x));
            if (result != null)
            {
                Fail($"op_Equality failed when left hand side is null at index {result.Value.Index}.");
            }
        }

        private void VerifyOpEqualityWithNotEqualItems()
        {
            if (OpEqualityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, ShiftedItems, (x, y) => !OpEqualityFunc(x, y));
            if (result != null)
            {
                Fail($"op_Equality failed with values expected to not be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyOpEqualityWithRightNull()
        {
            if (OpEqualityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, BaseItems, (x, _) => !OpEqualityFunc(x, default(T)));
            if (result != null)
            {
                Fail($"op_Equality failed when right hand side is null at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyOpInequalityWithEqualItems()
        {
            if (OpInequalityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, Items, (x, y) => !OpInequalityFunc(x, y));
            if (result != null)
            {
                Fail($"op_Inequality failed with values expected to be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyOpInequalityWithLeftNull()
        {
            if (OpInequalityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, BaseItems, (x, _) => OpInequalityFunc(default(T), x));
            if (result != null)
            {
                Fail($"op_Inequality failed when left hand side is null at index {result.Value.Index}.");
            }
        }

        private void VerifyOpInequalityWithNotEqualItems()
        {
            if (OpInequalityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, ShiftedItems, OpInequalityFunc);
            if (result != null)
            {
                Fail($"op_Inequality failed with values expected to not be equal at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }

        private void VerifyOpInequalityWithRightNull()
        {
            if (OpInequalityFunc == null)
            {
                return;
            }

            var result = CompareItems(BaseItems, BaseItems, (x, _) => OpInequalityFunc(x, default(T)));
            if (result != null)
            {
                Fail($"op_Inequality failed when right hand side is null at index {result.Value.Index}. "
                    + $"Expected: <{result.Value.Right}>. Actual: <{result.Value.Left}>.");
            }
        }
    }
}