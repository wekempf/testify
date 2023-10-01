namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class AcceptanceTestAttribute : CategoryAttribute
{
}
