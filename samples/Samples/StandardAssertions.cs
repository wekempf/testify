using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    public class StandardAssertions
    {
        // <isequalto>
        [Broken]
        [Fact(Skip = "Example test for documentation purposes.")]
        public void Ctor_GivenNames_SetsFulltName()
        {
            var sut = new Person("John", "Smith");

            Assert(sut.FullName).IsEqualTo("John Smith");

            // Expected "sut.FullName" to be "John Smith", but found "Smith, John".
        }
        // </isequalto>
    }
}
