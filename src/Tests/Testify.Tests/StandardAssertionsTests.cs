using System;
using Xunit;
using static Testify.Assertions;

namespace Testify
{
    public class StandardAssertionsTests
    {
        [Fact]
        public void IsEqual_GivenSameValues_ShouldNotThrow()
        {
            var value = DateTime.Now;
            Assert(value).IsEqualTo(value);
        }

        [Fact]
        public void IsEqualTo_Double_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(2.0d).IsEqualTo(1.0d, 0.05d, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Double_GivenMessageParameters_ShouldDisplayMessage()
        {
            try
            {
                Assert(2.0d).IsEqualTo(1.0d, 0.05d, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Double_GivenValuesThatDifferByDelta_ShouldNotThrow()
        {
            Assert(2.0d).IsEqualTo(1.0d, 1.0d);
        }

        [Fact]
        public void IsEqualTo_Double_GivenValuesThatDifferByLessThanDelta_ShouldNotThrow()
        {
            Assert(2.0d).IsEqualTo(1.0d, 1.5d);
        }

        [Fact]
        public void IsEqualTo_Double_GivenValuesThatDifferByMoreThanDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0d).IsEqualTo(1.0d, 0.05d);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Double_NaN_ShouldThrow()
        {
            try
            {
                Assert(double.NaN).IsEqualTo(1.0d, 1.5d);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEqualTo failed. Expected a difference no greater than <1.5> between expected value <1> and actual value <NaN>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenDifferentTypes_ShouldThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"IsEqualTo failed. Expected: <{expected} (System.TimeSpan)>. Actual: <{actual} (System.DateTime)>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenDifferentTypesAndMessage_ShouldThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsEqualTo failed. Expected: <{expected} (System.TimeSpan)>. Actual: <{actual} (System.DateTime)>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenDifferentTypesAndMessageParameters_ShouldThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsEqualTo failed. Expected: <{expected} (System.TimeSpan)>. Actual: <{actual} (System.DateTime)>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenDifferentValues_ShouldThrow()
        {
            var actual = DateTime.Now;
            var expected = actual + TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"IsEqualTo failed. Expected: <{expected}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenMessage_ShouldDisplayMessage()
        {
            var actual = DateTime.Now;
            var expected = actual + TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsEqualTo failed. Expected: <{expected}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            var actual = DateTime.Now;
            var expected = actual + TimeSpan.FromDays(1);
            try
            {
                Assert(actual).IsEqualTo(expected, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsEqualTo failed. Expected: <{expected}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Single_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(2.0f).IsEqualTo(1.0f, 0.05f, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Single_GivenMessageParameters_ShouldDisplayMessage()
        {
            try
            {
                Assert(2.0f).IsEqualTo(1.0f, 0.05f, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Single_GivenValuesThatDifferByDelta_ShouldNotThrow()
        {
            Assert(2.0f).IsEqualTo(1.0f, 1.0f);
        }

        [Fact]
        public void IsEqualTo_Single_GivenValuesThatDifferByLessThanDelta_ShouldNotThrow()
        {
            Assert(2.0f).IsEqualTo(1.0f, 1.5f);
        }

        [Fact]
        public void IsEqualTo_Single_GivenValuesThatDifferByMoreThanDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0f).IsEqualTo(1.0f, 0.05f);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEqualTo failed. Expected a difference no greater than <0.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsEqualTo_Single_NaN_ShouldThrow()
        {
            try
            {
                Assert(float.NaN).IsEqualTo(1.0f, 1.5f);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsEqualTo failed. Expected a difference no greater than <1.5> between expected value <1> and actual value <NaN>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsFalse_GivenFalse_ShouldNotThrow()
        {
            Assert(false).IsFalse();
        }

        [Fact]
        public void IsFalse_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert(true).IsFalse("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsFalse failed.");
                return;
            }

            Fail("IsFalse did not throw.");
        }

        [Fact]
        public void IsFalse_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(true).IsFalse("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsFalse failed.");
                return;
            }

            Fail("IsFalse did not throw.");
        }

        [Fact]
        public void IsFalse_GivenTrue_ShouldThrow()
        {
            try
            {
                Assert(true).IsFalse();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsFalse failed.");
                return;
            }

            Fail("IsFalse did not throw.");
        }

        [Fact]
        public void IsInstanceOfType_GivenCorrectType_ShouldNotThrow()
        {
            Assert("foo").IsInstanceOfType(typeof(string));
        }

        [Fact]
        public void IsInstanceOfType_GivenIncorrectType_ShouldThrow()
        {
            try
            {
                Assert("foo").IsInstanceOfType(typeof(Exception));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"IsInstanceOfType failed. Expected type: {typeof(Exception)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsInstanceOfType did not throw.");
        }

        [Fact]
        public void IsInstanceOfType_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").IsInstanceOfType(typeof(Exception), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsInstanceOfType failed. Expected type: {typeof(Exception)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsInstanceOfType did not throw.");
        }

        [Fact]
        public void IsInstanceOfType_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").IsInstanceOfType(typeof(Exception), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsInstanceOfType failed. Expected type: {typeof(Exception)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsInstanceOfType did not throw.");
        }

        [Fact]
        public void IsNotEqual_GivenDifferentValues_ShouldNotThrow()
        {
            var value = DateTime.Now;
            Assert(value).IsNotEqualTo(value + TimeSpan.FromDays(1));
        }

        [Fact]
        public void IsNotEqualTo_Double_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(2.0d).IsNotEqualTo(1.0d, 1.05d, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Double_GivenMessageParameters_ShouldDisplayMessage()
        {
            try
            {
                Assert(2.0d).IsNotEqualTo(1.0d, 1.05d, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Double_GivenValuesThatDifferByDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0d).IsNotEqualTo(1.0d, 1.0d);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEqualTo failed. Expected a difference greater than <1> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Double_GivenValuesThatDifferByLessThanDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0d).IsNotEqualTo(1.0d, 1.05d);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Double_GivenValuesThatDifferByMoreThanDelta_ShouldNotThrow()
        {
            Assert(2.0d).IsNotEqualTo(1.0d, 0.5d);
        }

        [Fact]
        public void IsNotEqualTo_GivenDifferentTypes_ShouldNotThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            Assert(actual).IsNotEqualTo(expected);
        }

        [Fact]
        public void IsNotEqualTo_GivenDifferentTypesAndMessage_ShouldNotThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            Assert(actual).IsNotEqualTo(expected, "Some message.");
        }

        [Fact]
        public void IsNotEqualTo_GivenDifferentTypesMessageParameters_ShouldNotThrow()
        {
            var actual = DateTime.Now;
            var expected = TimeSpan.FromDays(1);
            Assert(actual).IsNotEqualTo(expected, "Some {0}.", "message");
        }

        [Fact]
        public void IsNotEqualTo_GivenMessage_ShouldDisplayMessage()
        {
            var actual = DateTime.Now;
            try
            {
                Assert(actual).IsNotEqualTo(actual, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsNotEqualTo failed. Expected any value except: <{actual}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            var actual = DateTime.Now;
            try
            {
                Assert(actual).IsNotEqualTo(actual, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsNotEqualTo failed. Expected any value except: <{actual}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_GivenSameValues_ShouldThrow()
        {
            var actual = DateTime.Now;
            try
            {
                Assert(actual).IsNotEqualTo(actual);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"IsNotEqualTo failed. Expected any value except: <{actual}>. Actual: <{actual}>.");
                return;
            }

            Fail("IsEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Single_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(2.0f).IsNotEqualTo(1.0f, 1.05f, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Single_GivenMessageParameters_ShouldDisplayMessage()
        {
            try
            {
                Assert(2.0f).IsNotEqualTo(1.0f, 1.05f, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Single_GivenValuesThatDifferByDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0f).IsNotEqualTo(1.0f, 1.0f);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEqualTo failed. Expected a difference greater than <1> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Single_GivenValuesThatDifferByLessThanDelta_ShouldThrow()
        {
            try
            {
                Assert(2.0f).IsNotEqualTo(1.0f, 1.05f);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotEqualTo failed. Expected a difference greater than <1.05> between expected value <1> and actual value <2>.");
                return;
            }

            Fail("IsNotEqualTo did not throw.");
        }

        [Fact]
        public void IsNotEqualTo_Single_GivenValuesThatDifferByMoreThanDelta_ShouldNotThrow()
        {
            Assert(2.0f).IsNotEqualTo(1.0f, 0.5f);
        }

        [Fact]
        public void IsNotInstanceOfType_GivenCorrectType_ShouldThrow()
        {
            try
            {
                Assert("foo").IsNotInstanceOfType(typeof(string));
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"IsNotInstanceOfType failed. Wrong type: {typeof(string)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsNotInstanceOfType did not throw.");
        }

        [Fact]
        public void IsNotInstanceOfType_GivenIncorrectType_ShouldNotThrow()
        {
            Assert("foo").IsNotInstanceOfType(typeof(Exception));
        }

        [Fact]
        public void IsNotInstanceOfType_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert("foo").IsNotInstanceOfType(typeof(string), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsNotInstanceOfType failed. Wrong type: {typeof(string)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsNotInstanceOfType did not throw.");
        }

        [Fact]
        public void IsNotInstanceOfType_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert("foo").IsNotInstanceOfType(typeof(string), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage($"Some message. IsNotInstanceOfType failed. Wrong type: {typeof(string)}. Actual type: {typeof(string)}.");
                return;
            }

            Fail("IsNotInstanceOfType did not throw.");
        }

        [Fact]
        public void IsNotNull_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert<object>(null).IsNotNull("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotNull failed.");
                return;
            }

            Fail("IsNotNull did not throw.");
        }

        [Fact]
        public void IsNotNull_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert<object>(null).IsNotNull("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotNull failed.");
                return;
            }

            Fail("IsNotNull did not throw.");
        }

        [Fact]
        public void IsNotNull_GivenNull_ShouldThrow()
        {
            try
            {
                Assert<object>(null).IsNotNull();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotNull failed.");
                return;
            }

            Fail("IsNotNull did not throw.");
        }

        [Fact]
        public void IsNotNull_GivenReference_ShouldNotThrow()
        {
            Assert(new object()).IsNotNull();
        }

        [Fact]
        public void IsNotSameAs_GivenDifferentInstances_ShouldNotThrow()
        {
            Assert(new object()).IsNotSameAs(new object());
        }

        [Fact]
        public void IsNotSameAs_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                var obj = new object();
                Assert(obj).IsNotSameAs(obj, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSameAs failed.");
                return;
            }

            Fail("IsNotSameAs did not throw.");
        }

        [Fact]
        public void IsNotSameAs_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                var obj = new object();
                Assert(obj).IsNotSameAs(obj, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNotSameAs failed.");
                return;
            }

            Fail("IsNotSameAs did not throw.");
        }

        [Fact]
        public void IsNotSameAs_GivenSameInstances_ShouldThrow()
        {
            try
            {
                var obj = new object();
                Assert(obj).IsNotSameAs(obj);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNotSameAs failed.");
                return;
            }

            Fail("IsNotSameAs did not throw.");
        }

        [Fact]
        public void IsNull_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert(new object()).IsNull("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNull failed.");
                return;
            }

            Fail("IsNull did not throw.");
        }

        [Fact]
        public void IsNull_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new object()).IsNull("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsNull failed.");
                return;
            }

            Fail("IsNull did not throw.");
        }

        [Fact]
        public void IsNull_GivenNull_ShouldNotThrow()
        {
            Assert<object>(null).IsNull();
        }

        [Fact]
        public void IsNull_GivenReference_ShouldThrow()
        {
            try
            {
                Assert(new object()).IsNull();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsNull failed.");
                return;
            }

            Fail("IsNull did not throw.");
        }

        [Fact]
        public void IsSameAs_GivenDifferentInstances_ShouldThrow()
        {
            try
            {
                Assert(new object()).IsSameAs(new object());
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsSameAs failed.");
                return;
            }

            Fail("IsSameAs did not throw.");
        }

        [Fact]
        public void IsSameAs_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert(new object()).IsSameAs(new object(), "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSameAs failed.");
                return;
            }

            Fail("IsSameAs did not throw.");
        }

        [Fact]
        public void IsSameAs_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(new object()).IsSameAs(new object(), "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsSameAs failed.");
                return;
            }

            Fail("IsSameAs did not throw.");
        }

        [Fact]
        public void IsSameAs_GivenSameInstances_ShouldNotThrow()
        {
            var obj = new object();
            Assert(obj).IsSameAs(obj);
        }

        [Fact]
        public void IsTrue_GivenFalse_ShouldThrow()
        {
            try
            {
                Assert(false).IsTrue();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsTrue failed.");
                return;
            }

            Fail("IsTrue did not throw.");
        }

        [Fact]
        public void IsTrue_GivenMessage_ShouldDisplayMessage()
        {
            try
            {
                Assert(false).IsTrue("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsTrue failed.");
                return;
            }

            Fail("IsTrue did not throw.");
        }

        [Fact]
        public void IsTrue_GivenMessageParameters_ShouldDisplayFormattedMessage()
        {
            try
            {
                Assert(false).IsTrue("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsTrue failed.");
                return;
            }

            Fail("IsTrue did not throw.");
        }

        [Fact]
        public void IsTrue_GivenTrue_ShouldNotThrow()
        {
            Assert(true).IsTrue();
        }
    }
}