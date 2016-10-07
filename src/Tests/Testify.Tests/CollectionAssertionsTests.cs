using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class CollectionAssertionsTests
    {
        [Fact]
        public void AllItemsAreInstancesOfType_AllType_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(int));
        }

        [Fact]
        public void AllItemsAreInstancesOfType_MessageAllType_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(int), "Some message.");
        }

        [Fact]
        public void AllItemsAreInstancesOfType_MessageParametersAllType_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(int), "Some {0}.", "message");
        }

        [Fact]
        public void AllItemsAreInstancesOfType_NotAllType_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(string));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreInstancesOfType failed. Element at index 0 is not of expected type: <System.String>. Actual type: <System.Int32>.");
                return;
            }

            Fail("AllItemsAreInstancesOfType did not throw.");
        }

        [Fact]
        public void AllItemsAreInstancesOfType_MessageNotAllType_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(string), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreInstancesOfType failed. Element at index 0 is not of expected type: <System.String>. Actual type: <System.Int32>.");
                return;
            }

            Fail("AllItemsAreInstancesOfType did not throw.");
        }

        [Fact]
        public void AllItemsAreInstancesOfType_MessageParametersNotAllType_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).AllItemsAreInstancesOfType(typeof(string), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreInstancesOfType failed. Element at index 0 is not of expected type: <System.String>. Actual type: <System.Int32>.");
                return;
            }

            Fail("AllItemsAreInstancesOfType did not throw.");
        }

        [Fact]
        public void AllItemsAreNotNull_NoNull_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreNotNull();
        }

        [Fact]
        public void AllItemsAreNotNull_MessageNoNull_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreNotNull("Some message.");
        }

        [Fact]
        public void AllItemsAreNotNull_MessageParametersNoNull_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).AllItemsAreNotNull("Some {0}.", "message");
        }

        [Fact]
        public void AllItemsAreNotNull_Null_ShouldThrow()
        {
            try
            {
                Assert(new[] { "1", null, "3" }).AllItemsAreNotNull();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreNotNull failed.");
                return;
            }

            Fail("AllItemsAreNotNull did not throw.");
        }

        [Fact]
        public void AllItemsAreNotNull_MessageNull_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "1", null, "3" }).AllItemsAreNotNull("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreNotNull failed.");
                return;
            }

            Fail("AllItemsAreNotNull did not throw.");
        }

        [Fact]
        public void AllItemsAreNotNull_MessageParametersNull_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "1", null, "3" }).AllItemsAreNotNull("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreNotNull failed.");
                return;
            }

            Fail("AllItemsAreNotNull did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_UniqueItems_ShouldNotThrow()
        {
            Assert(Enumerable.Range(0, 10)).AllItemsAreUnique();
        }

        [Fact]
        public void AllItemsAreUnique_MessageUniqueItems_ShouldNotThrow()
        {
            Assert(new[] { "foo", "bar", "baz" }).AllItemsAreUnique("Some message.");
        }

        [Fact]
        public void AllItemsAreUnique_MessageParametersUniqueItems_ShouldNotThrow()
        {
            Assert(new[] { "foo", "bar", "baz" }).AllItemsAreUnique("Some {0}.", "message");
        }

        [Fact]
        public void AllItemsAreUnique_DuplicateNull_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_MessageDuplicateNull_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_MessageParametersDuplicateNull_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerDuplicateNull_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerMessageDuplicateNull_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerMessageParametersDuplicateNull_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", null, null }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <(null)>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_DuplicateItems_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar", "bar" }).AllItemsAreUnique();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreUnique failed. Duplicate item found: <bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_MessageDuplicateItems_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "bar" }).AllItemsAreUnique("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_MessageParametersDuplicateItems_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "bar" }).AllItemsAreUnique("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerDuplicateItems_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar", "Bar" }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("AllItemsAreUnique failed. Duplicate item found: <Bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerMessageDuplicateItems_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "Bar" }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <Bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void AllItemsAreUnique_ComparerMessageParametersDuplicateItems_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "Bar" }).AllItemsAreUnique(StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. AllItemsAreUnique failed. Duplicate item found: <Bar>.");
                return;
            }

            Fail("AllItemsAreUnique did not throw.");
        }

        [Fact]
        public void Contains_ContainedElement_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).Contains(2);
        }

        [Fact]
        public void Contains_MessageContainedElement_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).Contains(2, "Some message.");
        }

        [Fact]
        public void Contains_MessageParametersContainedElement_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).Contains(2, "Some {0}.", "message");
        }

        [Fact]
        public void Contains_ItemNotFound_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).Contains(4);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_MessageItemNotFound_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).Contains(4, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_MessageParametersItemNotFound_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).Contains(4, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_ComparerItemFound_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).Contains("bar", StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void Contains_MessageComparerItemFound_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).Contains("bar", StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void Contains_MessageParametersComparerItemFound_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).Contains("bar", StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void Contains_ComparerItemNotFound_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).Contains("baz", StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_MessageComparerItemNotFound_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).Contains("baz", StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void Contains_MessageParametersComparerItemNotFound_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).Contains("baz", StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Contains failed.");
                return;
            }

            Fail("Contains did not throw.");
        }

        [Fact]
        public void DoesNotContain_ItemNotFound_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).DoesNotContain(4);
        }

        [Fact]
        public void DoesNotContain_MessageItemNotFound_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).DoesNotContain(4, "Some message.");
        }

        [Fact]
        public void DoesNotContain_MessageParametersItemNotFound_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).DoesNotContain(4, "Some {0}.", "message");
        }

        [Fact]
        public void DoesNotContain_ItemFound_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).DoesNotContain(3);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void DoesNotContain_MessageItemFound_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).DoesNotContain(3, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void DoesNotContain_MessageParametersItemFound_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).DoesNotContain(3, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void DoesNotContain_ComparerItemFound_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).DoesNotContain("bar", StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void DoesNotContain_ComparerMessageItemFound_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).DoesNotContain("bar", StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void DoesNotContain_ComparerMessageParametersItemFound_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar" }).DoesNotContain("bar", StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. DoesNotContain failed.");
                return;
            }

            Fail("DoesNotContain did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_Equivalent_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 3, 2, 1 });
        }

        [Fact]
        public void IsEquivalentTo_MessageEquivalent_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 3, 2, 1 }, "Some message.");
        }

        [Fact]
        public void IsEquivalentTo_MessageParametersEquivalent_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 3, 2, 1 }, "Some {0}.", "message");
        }

        [Fact]
        public void IsEquivalentTo_SameReference_ShouldNotThrow()
        {
            var collection = new[] { 1, 2, 3 };
            Assert(collection).IsEquivalentTo(collection);
        }

        [Fact]
        public void IsEquivalentTo_MessageSameReference_ShouldNotThrow()
        {
            var collection = new[] { 1, 2, 3 };
            Assert(collection).IsEquivalentTo(collection, "Some message.");
        }

        [Fact]
        public void IsEquivalentTo_MessageParamtersSameReference_ShouldNotThrow()
        {
            var collection = new[] { 1, 2, 3 };
            Assert(collection).IsEquivalentTo(collection, "Some {0}.", "message");
        }

        [Fact]
        public void IsEquivalentTo_DifferentCount_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageDifferentCount_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageParametersDifferentCount_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MismatchedItems_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2, 4 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageMismatchedItems_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2, 4 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageParametersMismatchedItems_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsEquivalentTo(new[] { 1, 2, 4 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_ComparerEquivalent_ShouldNotThrow()
        {
            Assert(new[] { "foo", "bar" }).IsEquivalentTo(new[] { "foo", "Bar" }, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void IsEquivalentTo_ComparerMessageEquivalent_ShouldNotThrow()
        {
            Assert(new[] { "foo", "bar" }).IsEquivalentTo(new[] { "foo", "Bar" }, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsEquivalentTo_ComparerMessageParametersEquivalent_ShouldNotThrow()
        {
            Assert(new[] { "foo", "bar" }).IsEquivalentTo(new[] { "foo", "Bar" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsEquivalentTo_ComparerSameReference_ShouldNotThrow()
        {
            var collection = new[] { "foo", "bar" };
            Assert(collection).IsEquivalentTo(collection, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void IsEquivalentTo_ComparerMessageSameReference_ShouldNotThrow()
        {
            var collection = new[] { "foo", "bar" };
            Assert(collection).IsEquivalentTo(collection, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsEquivalentTo_ComparerMessageParamtersSameReference_ShouldNotThrow()
        {
            var collection = new[] { "foo", "bar" };
            Assert(collection).IsEquivalentTo(collection, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsEquivalentTo_ComparerDifferentCount_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_ComparerMessageDifferentCount_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageComparerParametersDifferentCount_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_ComparerMismatchedItems_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar", "buz" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageComparerMismatchedItems_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar", "buz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsEquivalentTo_MessageParametersComparerMismatchedItems_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "bar", "baz" }).IsEquivalentTo(new[] { "foo", "bar", "buz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEquivalentTo failed.");
                return;
            }

            Fail("IsEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 1, 2, 3, 4 });
        }

        [Fact]
        public void IsNotEquivalentTo_MessageMismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 1, 2, 3, 4 }, "Some message.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersMismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 1, 2, 3, 4 }, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotEquivalentTo_SameReference_ShouldThrow()
        {
            var collection = new[] { 1, 2, 3 };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageSameReference_ShouldDisplayMessage()
        {
            var collection = new[] { 1, 2, 3 };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersSameReference_ShouldDisplayFormattedMessage()
        {
            var collection = new[] { 1, 2, 3 };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_Equivalent_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 3, 2, 1 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageEquivalent_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 3, 2, 1 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersEquivalent_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotEquivalentTo(new[] { 3, 2, 1 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_ComparerMismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void IsNotEquivalentTo_MessageComparerMismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersComparerMismatchedItems_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotEquivalentTo_ComparerSameReference_ShouldThrow()
        {
            var collection = new[] { "foo", "Bar" };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageComparerSameReference_ShouldDisplayMessage()
        {
            var collection = new[] { "foo", "Bar" };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersComparerSameReference_ShouldDisplayFormattedMessage()
        {
            var collection = new[] { "foo", "Bar" };
            try
            {
                Assert(collection).IsNotEquivalentTo(collection, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_ComparerEquivalent_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "bar", "foo" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageComparerEquivalent_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "bar", "foo" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotEquivalentTo_MessageParametersComparerEquivalent_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotEquivalentTo(new[] { "bar", "foo" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEquivalentTo failed.");
                return;
            }

            Fail("IsNotEquivalentTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_NotEqual_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 3, 2 });
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageNotEqual_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 3, 2 }, "Some message.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersNotEqual_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 3, 2 }, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotSequenceEqualTo_Equal_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 2, 3 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageEqual_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 2, 3 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersEqual_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSequenceEqualTo(new[] { 1, 2, 3 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_SameReference_ShouldThrow()
        {
            try
            {
                var collection = new[] { 1, 2, 3 };
                Assert(collection).IsNotSequenceEqualTo(collection);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageSameReference_ShouldDisplayMessage()
        {
            try
            {
                var collection = new[] { 1, 2, 3 };
                Assert(collection).IsNotSequenceEqualTo(collection, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersSameReference_ShouldDisplayFormattedMessage()
        {
            try
            {
                var collection = new[] { 1, 2, 3 };
                Assert(collection).IsNotSequenceEqualTo(collection, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_ComparerNotEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar", "baz" });
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageComparerNotEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersComparerNotEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotSequenceEqualTo_ComparerEqual_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageComparerEqual_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersComparerEqual_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same elements.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_ComparerSameReference_ShouldThrow()
        {
            try
            {
                var collection = new[] { "foo", "Bar" };
                Assert(collection).IsNotSequenceEqualTo(collection, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageComparerSameReference_ShouldDisplayMessage()
        {
            try
            {
                var collection = new[] { "foo", "Bar" };
                Assert(collection).IsNotSequenceEqualTo(collection, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSequenceEqualTo_MessageParametersComparerSameReference_ShouldDisplayFormattedMessage()
        {
            try
            {
                var collection = new[] { "foo", "Bar" };
                Assert(collection).IsNotSequenceEqualTo(collection, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSequenceEqualTo failed. (Same reference.)");
                return;
            }

            Fail("IsNotSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_NotSubset_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2 });
        }

        [Fact]
        public void IsNotSubsetOf_MessageNotSubset_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2 }, "Some message.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageParametersNotSubset_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2 }, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotSubsetOf_Subset_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2, 3, 4 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageSubset_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2, 3, 4 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageParametersSubset_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsNotSubsetOf(new[] { 1, 2, 3, 4 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_ComparerNotSubset_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar", "Baz" }).IsNotSubsetOf(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void IsNotSubsetOf_MessageComparerNotSubset_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar", "Baz" }).IsNotSubsetOf(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageParametersComparerNotSubset_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar", "Baz" }).IsNotSubsetOf(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotSubsetOf_ComparerSubset_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSubsetOf(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageComparerSubset_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSubsetOf(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsNotSubsetOf_MessageParametersComparerSubset_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsNotSubsetOf(new[] { "foo", "bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSubsetOf failed.");
                return;
            }

            Fail("IsNotSubsetOf did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_Equal_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void IsSequenceEqualTo_MessageEqual_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 2, 3 }, "Some message.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageParametersEqual_ShouldNotThrow()
        {
            Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 2, 3 }, "Some {0}.", "message");
        }

        [Fact]
        public void IsSequenceEqualTo_DifferentLengths_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 2, 3, 4 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSequenceEqualTo failed. (Different number of elements.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageDifferentLengths_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 2, 3, 4 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Different number of elements.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_NotEqual_ShouldThrow()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 3, 2 });
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <3>. Actual element: <2>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageNotEqual_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 3, 2 }, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <3>. Actual element: <2>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageParametersNotEqual_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { 1, 2, 3 }).IsSequenceEqualTo(new[] { 1, 3, 2 }, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <3>. Actual element: <2>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_ComparerEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void IsSequenceEqualTo_MessageComparerEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some message.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageParametersComparerEqual_ShouldNotThrow()
        {
            Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "bar" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
        }

        [Fact]
        public void IsSequenceEqualTo_ComparerDifferentLengths_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "Bar", "baz" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSequenceEqualTo failed. (Different number of elements.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageComparerDifferentLengths_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "Bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Different number of elements.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageParametersComparerDifferentLengths_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "Bar", "baz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Different number of elements.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_ComparerNotEqual_ShouldThrow()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "baz" }, StringComparer.OrdinalIgnoreCase);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <baz>. Actual element: <Bar>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageComparerNotEqual_ShouldDisplayMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "baz" }, StringComparer.OrdinalIgnoreCase, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <baz>. Actual element: <Bar>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSequenceEqualTo_MessageParametersComparerNotEqual_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new[] { "foo", "Bar" }).IsSequenceEqualTo(new[] { "foo", "baz" }, StringComparer.OrdinalIgnoreCase, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSequenceEqualTo failed. (Elements at index 1 do not match. Expected element: <baz>. Actual element: <Bar>.)");
                return;
            }

            Fail("IsSequenceEqualTo did not throw.");
        }

        [Fact]
        public void IsSubsetOf_Subset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full);
        }

        [Fact]
        public void IsSubsetOf_ComparerSubset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full, EqualityComparer<int>.Default);
        }

        [Fact]
        public void IsSubsetOf_MessageSubset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full, "Some message.");
        }

        [Fact]
        public void IsSubsetOf_ComparerMessageSubset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full, EqualityComparer<int>.Default, "Some message.");
        }

        [Fact]
        public void IsSubsetOf_MessageParametersSubset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full, "Some {0}.", "message");
        }

        [Fact]
        public void IsSubsetOf_ComparerMessageParametersSubset_ShouldNotThrow()
        {
            var random = new Random(1337);
            var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

            Assert(partial).IsSubsetOf(full, EqualityComparer<int>.Default, "Some {0}.", "message");
        }

        [Fact]
        public void IsSubsetOf_NonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }

        [Fact]
        public void IsSubsetOf_ComparerNonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial, EqualityComparer<int>.Default);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }

        [Fact]
        public void IsSubsetOf_MessageNonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }

        [Fact]
        public void IsSubsetOf_ComparerMessageNonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial, EqualityComparer<int>.Default, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }

        [Fact]
        public void IsSubsetOf_MessageParametersNonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }

        [Fact]
        public void IsSubsetOf_ComparerMessageParametersNonSubset_ShouldThrow()
        {
            try
            {
                var random = new Random(1337);
                var full = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
                var partial = full.Take(5).OrderBy(_ => random.Next()).ToArray();

                Assert(full).IsSubsetOf(partial, EqualityComparer<int>.Default, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSubsetOf failed.");
                return;
            }

            Fail("IsSubsetOf did not throw.");
        }
    }
}