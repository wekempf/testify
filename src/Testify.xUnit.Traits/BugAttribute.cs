namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class BugAttribute : WorkItemAttribute
{
    public BugAttribute() { }

    public BugAttribute(string id) : base(id) { }

    public BugAttribute(long id) : base(id) { }
}
