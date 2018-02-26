using System.Threading.Tasks;
using Xunit;
using static Testify.Assertions;

namespace Testify.Tests
{
    public class TestCollectionTests
    {
        [Fact]
        public void Run_PassingTests_ShouldNotThrow()
        {
            var tests = new TestCollection();
            tests.AddTest("Test1", () => { });
            tests.AddTest("Test2", () => { });

            tests.RunTests("MyTests");
        }

        [Fact]
        public void Run_OneFailingTest_ShouldThrow()
        {
            var tests = new TestCollection();
            tests.AddTest("Test1", () => { });
            tests.AddTest("Test2", () => throw new AssertionException("xyzzy"));

            try
            {
                tests.RunTests("MyTests");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("MyTests");
                e.ExpectInnerAssertion("Test2 failed.");
                e.ExpectInnerAssertion("xyzzy");
                return;
            }

            Fail("RunTests did not throw.");
        }

        [Fact]
        public void Run_PassingAsyncTests_ShouldNotThrow()
        {
            var tests = new TestCollection();
            tests.AddTest("Test1", async () => await Task.Delay(1).ConfigureAwait(false));
            tests.AddTest("Test2", async () => await Task.Delay(1).ConfigureAwait(false));

            tests.RunTests("MyTests");
        }

        [Fact]
        public void Run_OneFailingAsyncTest_ShouldThrow()
        {
            var tests = new TestCollection();
            tests.AddTest("Test1", async () => await Task.Delay(1).ConfigureAwait(false));
            tests.AddTest("Test2", async () =>
            {
                await Task.Delay(1).ConfigureAwait(false);
                throw new AssertionException("xyzzy");
            });

            try
            {
                tests.RunTests("MyTests");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("MyTests");
                e.ExpectInnerAssertion("Test2 failed.");
                e.ExpectInnerAssertion("xyzzy");
                return;
            }

            Fail("RunTests did not throw.");
        }
    }
}