using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Microsoft;

namespace Testify;

#pragma warning disable CA1822

internal sealed class Guard
{
    private Guard() { }

    public static Guard Against { get; } = new();

    public T Null<T>(
        [NotNull][ValidatedNotNull] T? input,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        => input ?? throw new ArgumentNullException(parameterName);

    public string NullOrEmpty(
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        => string.IsNullOrEmpty(Null(input, parameterName))
            ? throw new ArgumentException("Value cannot be an empty string.", parameterName)
            : input;

    public string NullOrWhiteSpace(
        [NotNull][ValidatedNotNull] string? input,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        => string.IsNullOrWhiteSpace(Null(input, parameterName))
            ? throw new ArgumentException("Value cannot be an empty string or contain only white space.", parameterName)
            : input;
}
