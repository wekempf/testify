using System;
using System.Linq;
using static Testify.Assertions;

namespace Testify
{
    internal static class AssertionExceptionExtensions
    {
        internal static void ExpectMessage(this AssertionException exception, string message)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (exception.Message != message)
            {
                Fail("Unexpected message: '{0}'. Expected message: '{1}'.", exception.Message, message);
            }
        }

        internal static void ExpectInnerAssertion(this AssertionException exception, string message)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (!exception.InnerExceptions.OfType<AssertionException>().Any(e => e.Message == message))
            {
                Fail("Inner assertion not found. Message: {0}", message);
            }
        }
    }
}