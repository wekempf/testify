using Testify;
using static Testify.Assertions;

namespace Examples
{
    internal class Inclusion
    {
        private void Dummy() => Assert("foo").IsEqualTo("foo");
    }
}