using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    public record Person(string FirstName, string LastName, int Age=21)
    {
        public string FullName => $"{LastName}, {FirstName}";
    }
}
