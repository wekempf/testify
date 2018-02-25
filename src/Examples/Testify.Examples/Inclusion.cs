using Testify;
using static Testify.Assertions;

namespace Examples
{
    internal class Inclusion
    {
        internal void Dummy() => Assert("foo").IsEqualTo("foo");
    }
}