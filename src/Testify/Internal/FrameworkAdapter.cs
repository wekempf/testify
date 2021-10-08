namespace Testify.Internal;

/// <summary>
/// Base class for test framework adapters.
/// </summary>
internal abstract class FrameworkAdapter
{
    private static readonly Type[] KnownAdapterTypes = new[]
    {
            typeof(XunitFrameworkAdapter),
            typeof(FallbackFrameworkAdapter),
    };

    private static readonly Lazy<FrameworkAdapter> Adapter = new Lazy<FrameworkAdapter>(ProbeAdapter);

    private readonly Lazy<Assembly?> assembly;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkAdapter"/> class.
    /// </summary>
    /// <param name="assemblyName">The assembly name for the assembly with the test framework's assertion exception type.</param>
    /// <param name="exceptionName">The name of the test framework's assertion exception type.</param>
    protected FrameworkAdapter(string assemblyName, string exceptionName)
    {
        ArgumentNullException.ThrowIfNull(assemblyName);
        ArgumentNullException.ThrowIfNull(exceptionName);

        AssemblyName = assemblyName;
        ExceptionName = exceptionName;
        assembly = new Lazy<Assembly?>(() => GetAssembly());
    }

    /// <summary>
    /// Gets the assembly name for the assembly with the test framework's assertion exception type.
    /// </summary>
    public string AssemblyName { get; }

    /// <summary>
    /// Gets the name of the test framework's assertion exception type.
    /// </summary>
    public string ExceptionName { get; }

    /// <summary>
    /// Creates a new instance of the test framework's assertion exception type.
    /// </summary>
    /// <param name="message">The assertion message.</param>
    /// <returns>A new instance of the test framework's assertion exception type.</returns>
    public static Exception CreateException(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return Adapter.Value.CreateFrameworkException(message);
    }

    /// <summary>
    /// Creates a new instance of the test framework's assertion exception type.
    /// </summary>
    /// <param name="message">The assertion message.</param>
    /// <returns>A new instance of the test framework's assertion exception type.</returns>
    public virtual Exception CreateFrameworkException(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Exception? result = null;
        var exceptionType = assembly.Value?.GetType(ExceptionName);
        if (exceptionType != null)
        {
            result = (Exception?)Activator.CreateInstance(exceptionType, message);
        }

        return result ?? new AssertFailedException(message);
    }

    /// <summary>
    /// Gets the assembly with the test frameworks assertion exception type.
    /// </summary>
    /// <returns>The assembly with the test frameworks assertion exception type.</returns>
    protected virtual Assembly? GetAssembly()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName == AssemblyName)
            .FirstOrDefault();
    }

    private static FrameworkAdapter ProbeAdapter()
        => KnownAdapterTypes.Select(t => (FrameworkAdapter?)Activator.CreateInstance(t))
            .Where(a => a != null)
            .First() !;
}
