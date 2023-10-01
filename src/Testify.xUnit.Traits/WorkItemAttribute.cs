namespace Testify;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class WorkItemAttribute : CategoryAttribute
{
    public WorkItemAttribute()
        : this(string.Empty)
    {
    }

    public WorkItemAttribute(string id)
    {
        Id = id;
    }

    public WorkItemAttribute(long id)
        : this(id.ToString(CultureInfo.InvariantCulture))
    {
    }

    public string Id { get; }

    public override IEnumerable<KeyValuePair<string, string>> GetTraits()
    {
        foreach (var trait in base.GetTraits())
        {
            yield return trait;
        }

        if (!string.IsNullOrEmpty(Id))
        {
            yield return Trait(Name, Id);
        }
    }
}
