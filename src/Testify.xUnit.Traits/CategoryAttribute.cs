namespace Testify;

using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CategoryAttribute : TraitProviderAttribute
{
    private const string Category = "Category";
    private const string AttributeSuffix = "Attribute";

    protected CategoryAttribute()
    {
        var name = this.GetType().Name;
        if (name.EndsWith(AttributeSuffix))
        {
            name = name[..^AttributeSuffix.Length];
        }

        Name = name;
    }

    public CategoryAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override IEnumerable<KeyValuePair<string, string>> GetTraits()
    {
        yield return Trait(Category, Name);
    }

    protected static KeyValuePair<string, string> Trait(string key, string value)
        => new(key, value);
}
