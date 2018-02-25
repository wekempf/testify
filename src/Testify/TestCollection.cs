using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Testify
{
    /// <summary>
    /// Provides a collection of tests that can be run as one.
    /// </summary>
    /// <seealso cref="IEnumerable{Action}" />
    public class TestCollection : IEnumerable<Action>
    {
        private readonly List<Action> tests = new List<Action>();

        /// <summary>
        /// Adds the test.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="test">The test.</param>
        public void AddTest(string name, Action test)
        {
            Argument.NotNull(name, nameof(name));
            Argument.NotNull(test, nameof(test));

            tests.Add(() => InvokeTest(name, test));
        }

        /// <summary>
        /// Adds the test.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="test">The test.</param>
        public void AddTest(string name, Func<Task> test)
        {
            Argument.NotNull(name, nameof(name));
            Argument.NotNull(test, nameof(test));

            tests.Add(() => InvokeTest(name, test));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Action> GetEnumerator() => tests.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Runs the tests.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="AssertionException">One or more tests failed.</exception>
        public void RunTests(string message, params object[] args)
        {
            Argument.NotNull(message, nameof(message));

            Assertions.AssertAll(string.Format(CultureInfo.CurrentCulture, message, args), tests);
        }

        private static void InvokeTest(string name, Action test)
        {
            try
            {
                test();
            }
            catch (Exception e)
            {
                throw new AssertionException($"{name} failed.", e);
            }
        }

        private static void InvokeTest(string name, Func<Task> test) => InvokeTest(name, () => TestSynchronizationContext.Run(test));
    }
}