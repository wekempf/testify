namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class TaskAttribute : WorkItemAttribute
{
    public TaskAttribute() { }

    public TaskAttribute(string id) : base(id) { }

    public TaskAttribute(long id) : base(id) { }
}
