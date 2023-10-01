namespace Testify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class IssueAttribute : WorkItemAttribute
{
    public IssueAttribute() { }

    public IssueAttribute(string id) : base(id) { }

    public IssueAttribute(long id) : base(id) { }
}
