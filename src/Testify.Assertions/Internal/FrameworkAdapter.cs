namespace Testify.Internal;

using System;
using System.Linq;
using System.Reflection;

internal abstract class FrameworkAdapter
{
    private static readonly Type[] KnownAdapterTypes = new[]
    {
            typeof(XunitFrameworkAdapter),
            typeof(FallbackFrameworkAdapter),
    };

    private static readonly Lazy<FrameworkAdapter> Adapter = new(ProbeAdapter);

    private readonly Lazy<Assembly?> _assembly;

    protected FrameworkAdapter(string assemblyName, string exceptionName)
    {
        ArgumentNullException.ThrowIfNull(assemblyName);
        ArgumentNullException.ThrowIfNull(exceptionName);

        AssemblyName = assemblyName;
        ExceptionName = exceptionName;
        _assembly = new Lazy<Assembly?>(() => GetAssembly());
    }

    public string AssemblyName { get; }

    public string ExceptionName { get; }

    public static Exception CreateException(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return Adapter.Value.CreateFrameworkException(message);
    }

    public virtual Exception CreateFrameworkException(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Exception? result = null;
        var exceptionType = _assembly.Value?.GetType(ExceptionName);
        if (exceptionType != null)
        {
            result = (Exception?)Activator.CreateInstance(exceptionType, message);
        }

        return result ?? new AssertionException(message);
    }

    protected virtual Assembly? GetAssembly()
        => Array.Find(AppDomain.CurrentDomain.GetAssemblies(), a => a.FullName == AssemblyName);

    private static FrameworkAdapter ProbeAdapter()
        => KnownAdapterTypes.Select(t => (FrameworkAdapter?)Activator.CreateInstance(t))
            .First(a => a != null)!;
}
