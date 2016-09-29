using System;
using System.Threading;
using System.Threading.Tasks;
using static Testify.Assertions;
using static Testify.FrameworkMessages;

namespace Testify
{
    /// <summary>
    /// Provides assertion methods associated with <see cref="Task"/> or <see cref="Task{T}"/> in unit tests.
    /// </summary>
    public static class TaskAssertions
    {
        private const int DeadlockTimeout = 5000;
        private const int QuickTimeout = 100;

        /// <summary>
        /// Verifies that a <see cref="Task"/> completes.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task"/> type.</typeparam>
        /// <param name="task">The task to verify completes.</param>
        /// <remarks>
        /// <para>If this assertion fails you may have a deadlock or the task simply is taking too long
        /// to complete. If you need control over how long to wait before failing use one of the other overloads
        /// of this assertion.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void Completes<T>(this ActualValue<T> task)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.Completes(DeadlockTimeout, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task" /> completes.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify completes.</param>
        /// <param name="millisecondsTimeout">The amount of time in milliseconds to wait for the <see cref="Task"/> to complete.</param>
        /// <remarks>
        /// <para>If this assertion fails you may have a deadlock or the task simply is taking too long
        /// to complete.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void Completes<T>(this ActualValue<T> task, int millisecondsTimeout)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.Completes(millisecondsTimeout, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task" /> completes.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify completes.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <remarks>
        /// <para>If this assertion fails you may have a deadlock or the task simply is taking too long
        /// to complete. If you need control over how long to wait before failing use one of the other overloads
        /// of this assertion.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void Completes<T>(this ActualValue<T> task, string message)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.Completes(DeadlockTimeout, message, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task" /> completes.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify completes.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        /// <remarks>
        /// <para>If this assertion fails you may have a deadlock or the task simply is taking too long
        /// to complete. If you need control over how long to wait before failing use one of the other overloads
        /// of this assertion.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void Completes<T>(this ActualValue<T> task, string message, params object[] parameters)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.Completes(DeadlockTimeout, message, parameters);
        }

        /// <summary>
        /// Verifies that a <see cref="Task" /> completes.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify completes.</param>
        /// <param name="millisecondsTimeout">The amount of time in milliseconds to wait for the <see cref="Task"/> to complete.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        /// <remarks>
        /// <para>If this assertion fails you may have a deadlock or the task simply is taking too long
        /// to complete.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void Completes<T>(this ActualValue<T> task, int millisecondsTimeout, string message, params object[] parameters)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            if (!task.Value.WaitForCompletion(millisecondsTimeout))
            {
                Throw(nameof(Completes), TaskDidNotComplete(), message, parameters);
            }
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.IsBusy(QuickTimeout, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <param name="millisecondsTimeout">The amount of time in milliseconds to wait for the <see cref="Task"/>
        /// to complete to consider it busy.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task, int millisecondsTimeout)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.IsBusy(millisecondsTimeout, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task, string message)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.IsBusy(QuickTimeout, message, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task, string message, params object[] parameters)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.IsBusy(QuickTimeout, message, parameters);
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <param name="millisecondsTimeout">The amount of time in milliseconds to wait for the <see cref="Task"/>
        /// to complete to consider it busy.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task, int millisecondsTimeout, string message)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            task.IsBusy(millisecondsTimeout, message, null);
        }

        /// <summary>
        /// Verifies that a <see cref="Task"/> is busy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Task" /> type.</typeparam>
        /// <param name="task">The task to verify is busy.</param>
        /// <param name="millisecondsTimeout">The amount of time in milliseconds to wait for the <see cref="Task"/>
        /// to complete to consider it busy.</param>
        /// <param name="message">A message to display if the assertion fails. This message can
        /// be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        /// <remarks>
        /// <para>This assertion verifies that the task doesn't complete within a specified time.</para>
        /// <para>There are no overloads for working with <see cref="Task"/> collections, but you can easily
        /// make assertions by using the <see cref="m:Task.WhenAny"/> or <see cref="m:Task.WhenAll"/>
        /// methods. For example: <code>Assert(Task.WhenAll(tasks)).Complete();</code></para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> (or it's value) is <c>null</c>.</exception>
        /// <exception cref="AssertionException">The <paramref name="task"/> did not complete in the alloted time.</exception>
        public static void IsBusy<T>(this ActualValue<T> task, int millisecondsTimeout, string message, params object[] parameters)
            where T : Task
        {
            Argument.NotNull(task, nameof(task));
            Argument.NotNull(task.Value, nameof(task));

            if (task.Value.WaitForCompletion(millisecondsTimeout))
            {
                Throw(nameof(IsBusy), "Task completed unexpectedly.", message, parameters);
            }
        }

        private static WaitHandle GetWaitHandle(this Task task) =>
            ((IAsyncResult)task).AsyncWaitHandle;

        private static bool WaitForCompletion(this Task task, int millisecondsTimeout)
        {
            return task.GetWaitHandle().WaitOne(millisecondsTimeout);
        }
    }
}