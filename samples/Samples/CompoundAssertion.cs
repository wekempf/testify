namespace Samples
{
    public class CompoundAssertion
    {
        [Fact]
        public void Compound_Assertion_Sample()
        {
            var sut = new Person("John", "Smith");

            // <compoundassertion>
            AssertAll(
                Verify(sut.FirstName).IsEqualTo("John"),
                Verify(sut.LastName).IsEqualTo("Smith"));
            // </compoundassertion>
        }
    }
}
