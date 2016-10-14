using System.Collections.Generic;
using Testify;
using Xunit;
using static Testify.Assertions;

namespace Examples.Assertions
{
    public class CompoundExample
    {
        [Fact]
        public void Stack_Push_ShouldAddItem()
        {
            var stack = new Stack<int>();

            stack.Push(10);

            AssertAll(
                "Item was not added to the stack properly.",
                () => Assert(stack.Count)
                    .IsEqualTo(1, "Count was not incremented."),
                () => Assert(stack.Peek())
                    .IsEqualTo(10, "Top of the stack doesn't contain item."));
        }
    }
}