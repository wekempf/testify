namespace Samples
{
    public static class CustomAssertion
    {
        // <customassertion>
        public static AssertResult IsATeen(this IAssertContext<Person> context)
        {
            return context.ActualValue!.Age >= 13 && context.ActualValue!.Age <= 19
                ? context.Success
                : context.Failure($"{context.ActualExpression} was expected to be a teen, but age was {context.ActualValue}.");
        }
        // </customassertion>
    }
}
