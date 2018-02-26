using Moq;

namespace Testify
{
    /// <summary>
    /// Customizes <see cref="AnonymousData"/> to use Moq to automatically create mock objects for interface and abstract class
    /// types.
    /// </summary>
    /// <seealso cref="Testify.IAnonymousDataCustomization" />
    public class MoqCustomization : IAnonymousDataCustomization
    {
        /// <summary>
        /// Creates an object of a specified type.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if an object was created, otherwise <see langword="false"/>.</returns>
        public bool Create(IAnonymousDataContext context, out object result)
        {
            if (context.CallNextCustomization(out result))
            {
                return true;
            }

            if (context.ResultType.IsInterface() || context.ResultType.IsAbstract())
            {
                var mock = (Mock)context.Any(typeof(Mock<>).MakeGenericType(context.ResultType));
                result = mock.Object;
                return true;
            }

            return false;
        }
    }
}