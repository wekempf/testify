namespace Testify;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class UnitTestAttribute : CategoryAttribute
{
}
