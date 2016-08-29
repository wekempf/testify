using System;
using System.Linq;
using Xunit;
using static Testify.Assertions;

#pragma warning disable 0660
#pragma warning disable 0659
#pragma warning disable 0661

namespace Testify
{
    public class EquatableVerifierTests
    {
        [Fact]
        public void Verify_BrokenEquals_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenEquals>()
            {
                UniqueItemsFactory = () => new[] { new BrokenEquals(1), new BrokenEquals(2), new BrokenEquals(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Equals failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenEqualsTrue_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenEquals>()
            {
                UniqueItemsFactory = () => new[] { new BrokenEquals(1, true), new BrokenEquals(2, true), new BrokenEquals(3, true) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Equals failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenObjectEquals_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenObjectEquals>()
            {
                UniqueItemsFactory = () => new[] { new BrokenObjectEquals(1), new BrokenObjectEquals(2), new BrokenObjectEquals(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenObjectEqualsTrue_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenObjectEquals>()
            {
                UniqueItemsFactory = () => new[] { new BrokenObjectEquals(1, true), new BrokenObjectEquals(2, true), new BrokenObjectEquals(3, true) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpEquality_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenOpEquality>()
            {
                UniqueItemsFactory = () => new[] { new BrokenOpEquality(1), new BrokenOpEquality(2), new BrokenOpEquality(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("op_Equality failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpEqualityTrue_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenOpEquality>()
            {
                UniqueItemsFactory = () => new[] { new BrokenOpEquality(1, true), new BrokenOpEquality(2, true), new BrokenOpEquality(3, true) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("op_Equality failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpInEquality_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenOpInequality>()
            {
                UniqueItemsFactory = () => new[] { new BrokenOpInequality(1), new BrokenOpInequality(2), new BrokenOpInequality(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("op_Inequality failed with values expected to not be equal at index 0. Expected: <2>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_BrokenOpInequalityTrue_ShouldThrow()
        {
            var verifier = new EquatableVerifier<BrokenOpInequality>()
            {
                UniqueItemsFactory = () => new[] { new BrokenOpInequality(1, true), new BrokenOpInequality(2, true), new BrokenOpInequality(3, true) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("op_Inequality failed with values expected to be equal at index 0. Expected: <1>. Actual: <1>.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_Correct_ShouldNotThrow()
        {
            var verifier = new EquatableVerifier<Correct>
            {
                UniqueItemsFactory = () => new[] { new Correct(1), new Correct(2), new Correct(3) }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_FewerThan3UniqueItems_ShouldThrow()
        {
            var verifier = new EquatableVerifier<int>
            {
                UniqueItemsFactory = () => new[] { 1, 2 }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed. UniqueItemsFactory did not produce 3 or more items. IsTrue failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_Int32_ShouldNotThrow()
        {
            var verifier = new EquatableVerifier<int>
            {
                UniqueItemsFactory = () => new[] { 1, 2, 3, 4 }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_NoEqualityOperators_ShouldThrow()
        {
            var verifier = new EquatableVerifier<NoEqualityOperators>()
            {
                UniqueItemsFactory = () => new[] { new NoEqualityOperators(1), new NoEqualityOperators(2), new NoEqualityOperators(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Equality operators must be defined.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoGetHashCodeOverride_ShouldThrow()
        {
            var verifier = new EquatableVerifier<NoGetHashCodeOverride>()
            {
                UniqueItemsFactory = () => new[] { new NoGetHashCodeOverride(1), new NoGetHashCodeOverride(2), new NoGetHashCodeOverride(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Object.GetHashCode must be overridden.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoObjectEqualsOverride_ShouldThrow()
        {
            var verifier = new EquatableVerifier<NoObjectEqualsOverride>()
            {
                UniqueItemsFactory = () => new[] { new NoObjectEqualsOverride(1), new NoObjectEqualsOverride(2), new NoObjectEqualsOverride(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("Object.Equals must be overridden.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_NoUniqueItemsFactory_ShouldThrow()
        {
            var verifier = new EquatableVerifier<int>();

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed. UniqueItemsFactory is not set. IsNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_NullUniqueItem_ShouldThrow()
        {
            var verifier = new EquatableVerifier<string>
            {
                UniqueItemsFactory = () => new[] { "foo", null, "bar" }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed. UniqueItemsFactory should not produce null values. AllItemsAreNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_NullUniqueItems_ShouldThrow()
        {
            var verifier = new EquatableVerifier<int>
            {
                UniqueItemsFactory = () => null
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed. UniqueItemsFactory did not produce any items. IsNotNull failed.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        [Fact]
        public void Verify_String_ShouldNotThrow()
        {
            var verifier = new EquatableVerifier<string>
            {
                UniqueItemsFactory = () => new[] { "foo", "bar", "baz" }
            };

            verifier.Verify();
        }

        [Fact]
        public void Verify_UnstableGetHashCode_ShouldThrow()
        {
            var verifier = new EquatableVerifier<UnstableGetHashCode>()
            {
                UniqueItemsFactory = () => new[] { new UnstableGetHashCode(1), new UnstableGetHashCode(2), new UnstableGetHashCode(3) }
            };

            try
            {
                verifier.Verify();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("EquatableVerifier failed.");
                e.ExpectInnerAssertion("GetHashCode is not stable.");
                return;
            }

            Fail("EquatableVerifier did not throw.");
        }

        [Fact]
        public void Verify_UnstableUniqueItemsFactory_ShouldThrow()
        {
            var items = new[] { 1, 2, 3 };
            var verifier = new EquatableVerifier<int>
            {
                UniqueItemsFactory = () =>
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
                e.ExpectMessage("EquatableVerifier failed. UniqueItemsFactory is not stable. IsEqualTo failed. Expected: <4>. Actual: <5>.");
                return;
            }

            Fail("EquatableVerify did not throw.");
        }

        private class BrokenEquals : IEquatable<BrokenEquals>
        {
            private readonly bool result;

            public BrokenEquals(int value, bool result = false)
            {
                this.Value = value;
                this.result = result;
            }

            public int Value { get; }

            public static bool operator !=(BrokenEquals left, BrokenObjectEquals right) => !(left == right);

            public static bool operator ==(BrokenEquals left, BrokenObjectEquals right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : ((object)left).Equals(right);

            public override bool Equals(object obj)
            {
                var other = obj as BrokenEquals;
                return !object.ReferenceEquals(other, null) && this.Value == other.Value;
            }

            public bool Equals(BrokenEquals other) => this.result;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class BrokenObjectEquals : IEquatable<BrokenObjectEquals>
        {
            private readonly bool result;

            public BrokenObjectEquals(int value, bool result = false)
            {
                this.Value = value;
                this.result = result;
            }

            public int Value { get; }

            public static bool operator !=(BrokenObjectEquals left, BrokenObjectEquals right) => !(left == right);

            public static bool operator ==(BrokenObjectEquals left, BrokenObjectEquals right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public override bool Equals(object obj) => this.result;

            public bool Equals(BrokenObjectEquals other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class BrokenOpEquality : IEquatable<BrokenOpEquality>
        {
            private readonly bool result;

            public BrokenOpEquality(int value, bool result = false)
            {
                this.Value = value;
                this.result = result;
            }

            public int Value { get; }

            public static bool operator !=(BrokenOpEquality left, BrokenOpEquality right) => !(object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right));

            public static bool operator ==(BrokenOpEquality left, BrokenOpEquality right) => left.result;

            public override bool Equals(object obj) => this.Equals(obj as BrokenOpEquality);

            public bool Equals(BrokenOpEquality other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class BrokenOpInequality : IEquatable<BrokenOpInequality>
        {
            private readonly bool result;

            public BrokenOpInequality(int value, bool result = false)
            {
                this.Value = value;
                this.result = result;
            }

            public int Value { get; }

            public static bool operator !=(BrokenOpInequality left, BrokenOpInequality right) => left.result;

            public static bool operator ==(BrokenOpInequality left, BrokenOpInequality right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public override bool Equals(object obj) => this.Equals(obj as BrokenOpInequality);

            public bool Equals(BrokenOpInequality other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class Correct : IEquatable<Correct>
        {
            public Correct(int value)
            {
                this.Value = value;
            }

            public int Value { get; }

            public static bool operator !=(Correct left, Correct right) => !(left == right);

            public static bool operator ==(Correct left, Correct right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public override bool Equals(object obj) => this.Equals(obj as Correct);

            public bool Equals(Correct other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class NoEqualityOperators : IEquatable<NoEqualityOperators>
        {
            public NoEqualityOperators(int value)
            {
                this.Value = value;
            }

            public int Value { get; }

            public override bool Equals(object obj) => this.Equals(obj as NoEqualityOperators);

            public bool Equals(NoEqualityOperators other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class NoGetHashCodeOverride : IEquatable<NoGetHashCodeOverride>
        {
            public NoGetHashCodeOverride(int value)
            {
                this.Value = value;
            }

            public int Value { get; }

            public static bool operator !=(NoGetHashCodeOverride left, NoGetHashCodeOverride right) => !(left == right);

            public static bool operator ==(NoGetHashCodeOverride left, NoGetHashCodeOverride right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public override bool Equals(object obj) => this.Equals(obj as NoGetHashCodeOverride);

            public bool Equals(NoGetHashCodeOverride other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override string ToString() => this.Value.ToString();
        }

        private class NoObjectEqualsOverride : IEquatable<NoObjectEqualsOverride>
        {
            public NoObjectEqualsOverride(int value)
            {
                this.Value = value;
            }

            public int Value { get; }

            public static bool operator !=(NoObjectEqualsOverride left, NoObjectEqualsOverride right) => !(left == right);

            public static bool operator ==(NoObjectEqualsOverride left, NoObjectEqualsOverride right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public bool Equals(NoObjectEqualsOverride other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.Value.GetHashCode();

            public override string ToString() => this.Value.ToString();
        }

        private class UnstableGetHashCode : IEquatable<UnstableGetHashCode>
        {
            private int hasCode;

            public UnstableGetHashCode(int value)
            {
                this.Value = value;
            }

            public int Value { get; }

            public static bool operator !=(UnstableGetHashCode left, UnstableGetHashCode right) => !(left == right);

            public static bool operator ==(UnstableGetHashCode left, UnstableGetHashCode right) => object.ReferenceEquals(left, null) ? object.ReferenceEquals(right, null) : left.Equals(right);

            public override bool Equals(object obj) => this.Equals(obj as UnstableGetHashCode);

            public bool Equals(UnstableGetHashCode other) => !object.ReferenceEquals(other, null) && this.Value == other.Value;

            public override int GetHashCode() => this.hasCode++;

            public override string ToString() => this.Value.ToString();
        }
    }
}