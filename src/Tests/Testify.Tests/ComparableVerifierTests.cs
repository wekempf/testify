using System;
using System.Linq;
using Xunit;
using static Testify.Assertions;

#pragma warning disable 0660
#pragma warning disable 0659
#pragma warning disable 0661

namespace Testify
{
    public class ComparableVerifierTests
    {
        [Fact]
        public void Verify_Int32_ShouldNotThrow()
        {
            var verifier = new ComparableVerifier<int>
            {
                OrderedItemsFactory = () => new[] { 1, 2, 3, 4 }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_String_ShouldNotThrow()
        {
            var verifier = new ComparableVerifier<string>
            {
                OrderedItemsFactory = () => new[] { "bar", "baz", "foo" }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_Correct_ShouldNotThrow()
        {
            var verifier = new ComparableVerifier<Correct>
            {
                OrderedItemsFactory = () => new[] { new Correct(1), new Correct(2), new Correct(3) }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_NoOrderedItemsFactory_ShouldThrow()
        {
            var verifier = new ComparableVerifier<int>();

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed. OrderedItemsFactory is not set. IsNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_NullUniqueItems_ShouldThrow()
        {
            var verifier = new ComparableVerifier<int>
            {
                OrderedItemsFactory = () => null
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed. OrderedItemsFactory did not produce any items. IsNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_FewerThan3UniqueItems_ShouldThrow()
        {
            var verifier = new ComparableVerifier<int>
            {
                OrderedItemsFactory = () => new[] { 1, 2 }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed. OrderedItemsFactory did not produce 3 or more items. IsTrue failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_NullUniqueItem_ShouldThrow()
        {
            var verifier = new ComparableVerifier<string>
            {
                OrderedItemsFactory = () => new[] { "foo", null, "bar" }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed. OrderedItemsFactory should not produce null values. AllItemsAreNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_UnstableOrderedItemsFactory_ShouldThrow()
        {
            var items = new[] { 1, 2, 3 };
            var verifier = new ComparableVerifier<int>
            {
                OrderedItemsFactory = () =>
                {
                    items = items.Concat(new[] { 1 }).ToArray();
                    return items;
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed. OrderedItemsFactory is not stable. IsEqualTo failed. Expected: <4>. Actual: <5>.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_NoObjectEqualsOverride_ShouldThrow()
        {
            var verifier = new ComparableVerifier<NoObjectEqualsOverride>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new NoObjectEqualsOverride(1),
                    new NoObjectEqualsOverride(2),
                    new NoObjectEqualsOverride(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals must be overridden.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoGetHashCodeOverride_ShouldThrow()
        {
            var verifier = new ComparableVerifier<NoGetHashCodeOverride>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new NoGetHashCodeOverride(1),
                    new NoGetHashCodeOverride(2),
                    new NoGetHashCodeOverride(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Object.GetHashCode must be overridden.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoEqualityOperators_ShouldThrow()
        {
            var verifier = new ComparableVerifier<NoEqualityOperators>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new NoEqualityOperators(1),
                    new NoEqualityOperators(2),
                    new NoEqualityOperators(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Equality operators must be defined.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoComparisonOperators_ShouldThrow()
        {
            var verifier = new ComparableVerifier<NoComparisonOperators>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new NoComparisonOperators(1),
                    new NoComparisonOperators(2),
                    new NoComparisonOperators(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Comparison operators must be defined.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_UnstableGetHashCode_ShouldThrow()
        {
            var verifier = new ComparableVerifier<UnstableGetHashCode>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new UnstableGetHashCode(1),
                    new UnstableGetHashCode(2),
                    new UnstableGetHashCode(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("GetHashCode is not stable.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenObjectEquals_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenObjectEquals>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenObjectEquals(1),
                    new BrokenObjectEquals(2),
                    new BrokenObjectEquals(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenObjectEqualsTrue_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenObjectEquals>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenObjectEquals(1, true),
                    new BrokenObjectEquals(2, true),
                    new BrokenObjectEquals(3, true)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenEquals_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenEquals>()
            {
                OrderedItemsFactory = () => new[] { new BrokenEquals(1), new BrokenEquals(2), new BrokenEquals(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("IEquatable<BrokenEquals>.Equals failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenEqualsTrue_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenEquals>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenEquals(1, true),
                    new BrokenEquals(2, true),
                    new BrokenEquals(3, true)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("IEquatable<BrokenEquals>.Equals failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpEquality_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenOpEquality>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenOpEquality(1),
                    new BrokenOpEquality(2),
                    new BrokenOpEquality(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("op_Equality failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpEqualityTrue_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenOpEquality>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenOpEquality(1, true),
                    new BrokenOpEquality(2, true),
                    new BrokenOpEquality(3, true)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("op_Equality failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpInEquality_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenOpInequality>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenOpInequality(1),
                    new BrokenOpInequality(2),
                    new BrokenOpInequality(3)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("op_Inequality failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpInequalityTrue_ShouldThrow()
        {
            var verifier = new ComparableVerifier<BrokenOpInequality>()
            {
                OrderedItemsFactory = () => new[]
                {
                    new BrokenOpInequality(1, true),
                    new BrokenOpInequality(2, true),
                    new BrokenOpInequality(3, true)
                }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("ComparableVerifier failed.");
                e.ExpectInnerAssertion("op_Inequality failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("ComparableVerifier did not throw.");
        }

        private class Correct : IEquatable<Correct>, IComparable<Correct>, IComparable
        {
            public Correct(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as Correct);

            public bool Equals(Correct other) => !ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(Correct other)
            {
                if (other == null)
                {
                    return 1;
                }

                return Value.CompareTo(other.Value);
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                var other = obj as Correct;
                if (other == null)
                {
                    throw new ArgumentException("Object is not a Correct");
                }

                return CompareTo(other);
            }

            public static bool operator ==(Correct left, Correct right) =>
                object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(Correct left, Correct right) => !(left == right);

            public static bool operator >(Correct left, Correct right) =>
                object.ReferenceEquals(left, null) ? false : left.CompareTo(right) > 0;

            public static bool operator <(Correct left, Correct right) =>
                object.ReferenceEquals(left, null) ? !object.ReferenceEquals(right, null) : left.CompareTo(right) < 0;

            public static bool operator >=(Correct left, Correct right) =>
                object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.CompareTo(right) >= 0;

            public static bool operator <=(Correct left, Correct right) =>
                object.ReferenceEquals(left, null) ? true : left.CompareTo(right) <= 0;
        }

        private class NoObjectEqualsOverride
            : IEquatable<NoObjectEqualsOverride>, IComparable<NoObjectEqualsOverride>, IComparable
        {
            public NoObjectEqualsOverride(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public bool Equals(NoObjectEqualsOverride other) =>
                !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(NoObjectEqualsOverride other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(NoObjectEqualsOverride left, NoObjectEqualsOverride right) =>
                object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(NoObjectEqualsOverride left, NoObjectEqualsOverride right) =>
                !(left == right);
        }

        private class NoGetHashCodeOverride
            : IEquatable<NoGetHashCodeOverride>, IComparable<NoGetHashCodeOverride>, IComparable
        {
            public NoGetHashCodeOverride(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override bool Equals(object obj) => Equals(obj as NoGetHashCodeOverride);

            public bool Equals(NoGetHashCodeOverride other) =>
                !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(NoGetHashCodeOverride other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(NoGetHashCodeOverride left, NoGetHashCodeOverride right) =>
                object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(NoGetHashCodeOverride left, NoGetHashCodeOverride right) =>
                !(left == right);
        }

        private class NoEqualityOperators : IEquatable<NoEqualityOperators>, IComparable<NoEqualityOperators>, IComparable
        {
            public NoEqualityOperators(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as NoEqualityOperators);

            public bool Equals(NoEqualityOperators other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(NoEqualityOperators other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();
        }

        private class NoComparisonOperators : IEquatable<NoComparisonOperators>, IComparable<NoComparisonOperators>, IComparable
        {
            public NoComparisonOperators(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as NoComparisonOperators);

            public bool Equals(NoComparisonOperators other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(NoComparisonOperators other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(NoComparisonOperators left, NoComparisonOperators right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(NoComparisonOperators left, NoComparisonOperators right) => !(left == right);
        }

        private class UnstableGetHashCode : IEquatable<UnstableGetHashCode>, IComparable<UnstableGetHashCode>, IComparable
        {
            private int hasCode;

            public UnstableGetHashCode(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => hasCode++;

            public override bool Equals(object obj) => Equals(obj as UnstableGetHashCode);

            public bool Equals(UnstableGetHashCode other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(UnstableGetHashCode other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(UnstableGetHashCode left, UnstableGetHashCode right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(UnstableGetHashCode left, UnstableGetHashCode right) => !(left == right);
        }

        private class BrokenObjectEquals : IEquatable<BrokenObjectEquals>, IComparable<BrokenObjectEquals>, IComparable
        {
            private readonly bool result;

            public BrokenObjectEquals(int value, bool result = false)
            {
                Value = value;
                this.result = result;
            }

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => result;

            public bool Equals(BrokenObjectEquals other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(BrokenObjectEquals other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(BrokenObjectEquals left, BrokenObjectEquals right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(BrokenObjectEquals left, BrokenObjectEquals right) => !(left == right);
        }

        private class BrokenEquals : IEquatable<BrokenEquals>, IComparable<BrokenEquals>, IComparable
        {
            private readonly bool result;

            public BrokenEquals(int value, bool result = false)
            {
                Value = value;
                this.result = result;
            }

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj)
            {
                var other = obj as BrokenEquals;
                return !object.ReferenceEquals(other, null) && Value == other.Value;
            }

            public bool Equals(BrokenEquals other) => result;

            public int CompareTo(BrokenEquals other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(BrokenEquals left, BrokenObjectEquals right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : ((object)left).Equals(right);

            public static bool operator !=(BrokenEquals left, BrokenObjectEquals right) => !(left == right);
        }

        private class BrokenOpEquality : IEquatable<BrokenOpEquality>, IComparable<BrokenOpEquality>, IComparable
        {
            private readonly bool result;

            public BrokenOpEquality(int value, bool result = false)
            {
                Value = value;
                this.result = result;
            }

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as BrokenOpEquality);

            public bool Equals(BrokenOpEquality other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(BrokenOpEquality other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(BrokenOpEquality left, BrokenOpEquality right) => left.result;

            public static bool operator !=(BrokenOpEquality left, BrokenOpEquality right) => !(object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right));
        }

        private class BrokenOpInequality : IEquatable<BrokenOpInequality>, IComparable<BrokenOpInequality>, IComparable
        {
            private readonly bool result;

            public BrokenOpInequality(int value, bool result = false)
            {
                Value = value;
                this.result = result;
            }

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as BrokenOpInequality);

            public bool Equals(BrokenOpInequality other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(BrokenOpInequality other) => throw new NotImplementedException();

            public int CompareTo(object obj) => throw new NotImplementedException();

            public static bool operator ==(BrokenOpInequality left, BrokenOpInequality right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(BrokenOpInequality left, BrokenOpInequality right) => left.result;
        }

        private class BrokenOpGreaterThan : IEquatable<BrokenOpGreaterThan>, IComparable<BrokenOpGreaterThan>, IComparable
        {
            public BrokenOpGreaterThan(int value) => Value = value;

            public int Value { get; }

            public override string ToString() => Value.ToString();

            public override int GetHashCode() => Value.GetHashCode();

            public override bool Equals(object obj) => Equals(obj as BrokenOpGreaterThan);

            public bool Equals(BrokenOpGreaterThan other) => !object.ReferenceEquals(other, null) && Value == other.Value;

            public int CompareTo(BrokenOpGreaterThan other)
            {
                if (other == null)
                {
                    return 1;
                }

                return Value.CompareTo(other.Value);
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                var other = obj as BrokenOpGreaterThan;
                if (other == null)
                {
                    throw new ArgumentException("Object is not a BrokenOpGreaterThan");
                }

                return CompareTo(other);
            }

            public static bool operator ==(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public static bool operator !=(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => !(left == right);

            public static bool operator >(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => false;

            public static bool operator <(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => object.ReferenceEquals(left, null) ? !object.ReferenceEquals(right, null) : left.CompareTo(right) < 0;

            public static bool operator >=(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.CompareTo(right) > 0;

            public static bool operator <=(BrokenOpGreaterThan left, BrokenOpGreaterThan right) => object.ReferenceEquals(left, null) ? true : left.CompareTo(right) < 0;
        }
    }
}