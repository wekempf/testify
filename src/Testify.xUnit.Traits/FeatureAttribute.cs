namespace Testify;

using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class FeatureAttribute : WorkItemAttribute
{
    public FeatureAttribute() { }

    public FeatureAttribute(string id) : base(id) { }

    public FeatureAttribute(long id) : base(id) { }
}
