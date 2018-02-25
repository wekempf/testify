using Testify;
using Xunit;

namespace Examples.UsingVerifiers
{
    public class PointTests
    {
        [Fact]
        public void VerifyComparableContract()
        {
            var verifier = new ComparableVerifier<Point>
            {
                OrderedItemsFactory = () => new[]
                {
                    new Point(0, 0),
                    new Point(0, 1),
                    new Point(1, 0),
                    new Point(1, 1),
                },
            };
            verifier.Verify();
        }
    }
}