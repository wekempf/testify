---
uid: Home
---

# Testify
Testify is a .NET unit testing helper framework providing fluent assertions, contract verifiers and an anonymous data factory.

## Quick Start Notes:
1. Create a unit test project using your favorite unit testing framework. Testify is framework agnostic and should work with any .NET unit testing framework.

1. Install the Testify NuGet package into this project.

	```PowerShell
	Install-Package Testify
	```

1. In a unit test source file add the following using statements.

	```csharp
	using Testify;
	using static Testify.Assertions;
	```

1. In a unit test use the fluent syntax for declaring assertions, nothing the rich IntelliSense support.

	```csharp
	Assert(testResult).IsEqualTo(expectedValue);
	```

1. Read the @Api.

1. See the project at [GitHub](https://github.com/wekempf/testify)
