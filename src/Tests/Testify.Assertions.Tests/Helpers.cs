namespace Testify.Tests;

using Xunit.Sdk;

internal static class Helpers
{
    public static Action Act(Action action) => action;

    public static void ShouldSucceed(Action action)
    {
        var exception = Record.Exception(action);
        if (exception != null)
        {
            throw new XunitException($"Expected assertion to succeed, but failed with {exception.GetType().Name}.");
        }
    }

    public static void ShouldFail(Action action, string expectedMessage)
    {
        var exception = Record.Exception(action)
            ?? throw new XunitException("Expected assertion to fail, but succeeded.");

        XAssert.Equal(expectedMessage, exception.Message);
    }
}
