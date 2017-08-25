using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Provides a SynchronizationContext that can be used within a synchronous test method.
    /// </summary>
    /// <seealso cref="System.Threading.SynchronizationContext" />
    public class TestSynchronizationContext : SynchronizationContext
    {
        private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> queue
            = new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

        private TestSynchronizationContext()
        {
        }

        /// <summary>
        /// Runs the specified function.
        /// </summary>
        /// <param name="func">The function.</param>
        public static void Run(Func<Task> func)
        {
            Argument.NotNull(func, nameof(func));

            var previousContext = SynchronizationContext.Current;
            try
            {
                var context = new TestSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(context);

                var task = func();
                task.ContinueWith(_ => context.queue.CompleteAdding(), TaskScheduler.Default);

                context.RunOnCurrentThread();

                task.GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(previousContext);
            }
        }

        /// <summary>
        /// When overridden in a derived class, dispatches an asynchronous message to a synchronization context.
        /// </summary>
        /// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Post(SendOrPostCallback d, object state)
        {
            Argument.NotNull(d, nameof(d));

            queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        private void RunOnCurrentThread()
        {
            while (queue.TryTake(out var workItem, Timeout.Infinite))
            {
                workItem.Key.Invoke(workItem.Value);
            }
        }
    }
}