using System.Collections.Generic;
using System.Linq;
using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.UsingAnonymousData
{
    public class SimpleExample
    {
        [Fact]
        public void IndexOf_ItemInList_ShouldReturnIndex()
        {
            const int item = 10;
            var anon = new AnonymousData();
            var front = anon.AnyEnumerable<int>().ToArray();
            var back = anon.AnyEnumerable<int>().ToArray();
            var list = new List<int>(front)
            {
                item,
            };
            list.AddRange(back);

            var index = list.IndexOf(item);

            Assert(index).IsEqualTo(front.Length);
        }
    }
}