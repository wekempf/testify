namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class UserStoryAttribute : WorkItemAttribute
{
    public UserStoryAttribute() { }

    public UserStoryAttribute(string id) : base(id) { }

    public UserStoryAttribute(long id) : base(id) { }
}
