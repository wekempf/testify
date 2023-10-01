namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class EpicAttribute : WorkItemAttribute
{
    public EpicAttribute() { }

    public EpicAttribute(string id) : base(id) { }

    public EpicAttribute(long id) : base(id) { }
}
