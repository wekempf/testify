using System.Threading.Tasks;
using Xunit;
using static Testify.Assertions;

namespace Testify.Tests
{
    public class TaskAssertionsTests
    {
        [Fact]
        public void Completes_Completed_ShouldNotThrow() => Assert(Task.FromResult(1)).Completes();

        [Fact]
        public void Completes_MessageCompleted_ShouldNotThrow() =>
            Assert(Task.FromResult(1)).Completes("Some message.");

        [Fact]
        public void Completes_MessageParametersCompleted_ShouldNotThrow() =>
            Assert(Task.FromResult(1)).Completes("Some {0}.", "message");

        [Fact]
        public void Completes_InTime_ShouldNotThrow() =>
            Assert(Task.Delay(100)).Completes();

        [Fact]
        public void Completes_MessageInTime_ShouldNotThrow() => Assert(Task.Delay(100)).Completes("Some message.");

        [Fact]
        public void Completes_MessageParametersInTime_ShouldNotThrow() =>
            Assert(Task.Delay(100)).Completes("Some {0}.", "message");

        [Fact]
        public void Completes_Uncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void Completes_MessageUncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void Completes_MessageParametersUncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void Completes_TimeoutCompleted_ShouldNotThrow() => Assert(Task.FromResult(1)).Completes(1);

        [Fact]
        public void Completes_TimeoutMessageCompleted_ShouldNotThrow() =>
            Assert(Task.FromResult(1)).Completes(1, "Some message.");

        [Fact]
        public void Completes_TimeoutMessageParametersCompleted_ShouldNotThrow() =>
            Assert(Task.FromResult(1)).Completes(1, "Some {0}.", "message");

        [Fact]
        public void Completes_TimeoutInTime_ShouldNotThrow() => Assert(Task.Delay(100)).Completes(200);

        [Fact]
        public void Completes_TimeoutMessageInTime_ShouldNotThrow() =>
            Assert(Task.Delay(100)).Completes(1000, "Some message.");

        [Fact]
        public void Completes_TimeoutMessageParametersInTime_ShouldNotThrow() =>
            Assert(Task.Delay(100)).Completes(1000, "Some {0}.", "message");

        [Fact]
        public void Completes_TimeoutUncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes(100);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void Completes_TimeoutMessageUncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes(100, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void Completes_TimeoutMessageParametersUncompleted_Throws()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                Assert(tcs.Task).Completes(100, "Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. Completes failed. Task did not complete within allotted time and may be deadlocked.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void IsBusy_Busy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy();
        }

        [Fact]
        public void IsBusy_MessageBusy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy("Some message.");
        }

        [Fact]
        public void IsBusy_MessageParametersBusy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy("Some {0}.", "message");
        }

        [Fact]
        public void IsBusy_Completed_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy();
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("IsBusy did not throw.");
        }

        [Fact]
        public void IsBusy_MessageCompleted_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy("Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void IsBusy_MessageParametersCompleted_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void IsBusy_TimeoutBusy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy(50);
        }

        [Fact]
        public void IsBusy_TimeoutMessageBusy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy(50, "Some message.");
        }

        [Fact]
        public void IsBusy_TimeoutMessageParametersBusy_ShouldNotThrow()
        {
            var tcs = new TaskCompletionSource<bool>();
            Assert(tcs.Task).IsBusy(50, "Some {0}.", "message");
        }

        [Fact]
        public void IsBusy_TimeoutCompleted_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy(50);
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("IsBusy did not throw.");
        }

        [Fact]
        public void IsBusy_TimeoutMessageCompleted_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy(50, "Some message.");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("Completes did not throw.");
        }

        [Fact]
        public void IsBusy_TimeoutMessageParametersCompleted_Throws()
        {
            try
            {
                Assert(Task.FromResult(1)).IsBusy("Some {0}.", "message");
            }
            catch (AssertionException e)
            {
                e.ExpectMessage("Some message. IsBusy failed. Task completed unexpectedly.");
                return;
            }

            Fail("Completes did not throw.");
        }
    }
}