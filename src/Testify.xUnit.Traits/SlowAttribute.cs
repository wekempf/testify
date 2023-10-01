namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SlowAttribute : CategoryAttribute
{
}
