---
uid: Home
---

# Testify

Testify is a .NET unit testing helper framework providing fluent assertions, contract verifiers and an anonymous data factory.

## Testify V2

This is a breaking version change that requires .NET 6 or newer. For more information on why the breaking changes see @WhyV2. For more information on the changes see @V2BreakingChanges.

## Quick Start Notes

1. Create a unit test project using your favorite unit testing framework. Testify is framework agnostic and should work with any .NET unit testing framework.

1. Install the Testify NuGet package into this project.

    ```PowerShell
    Install-Package Testify
    ```

1. Add a `Usings.cs` file add the following using statements.

    [!Include[ExampleInclusion](articles/examples/inclusion.md)]

1. In a unit test use the fluent syntax for declaring assertions, noting the rich IntelliSense support.

    ```csharp
    Assert(testResult).IsEqualTo(expectedValue);
    ```

1. Read the @Intro and @Api.

1. See the project at [GitHub](https://github.com/wekempf/testify)