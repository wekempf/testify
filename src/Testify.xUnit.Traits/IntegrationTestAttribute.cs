namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class IntegrationTestAttribute : CategoryAttribute
{
}
