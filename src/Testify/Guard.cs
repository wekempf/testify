namespace Testify;

/// <summary>
/// Argument validation guard class.
/// </summary>
[DebuggerStepThrough]
internal sealed class Guard
{
    private Guard()
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="Guard"/> class.
    /// </summary>
    public static Guard Against { get; } = new Guard();

    /// <summary>
    /// Validates the argument is not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The argument type.</typeparam>
    /// <param name="value">The argument value.</param>
    /// <param name="paramName">The name of the argument.</param>
    /// <returns>The original argument value.</returns>
    /// <exception cref="ArgumentNullException">The argument was <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Null<T>([NotNull] T? value, [CallerArgumentExpression("value")] string? paramName = default)
        where T : class
        => value == null ? throw new ArgumentNullException(paramName) : value;

    /// <summary>
    /// Validates the argument is not <see langword="null"/> or an empty string.
    /// </summary>
    /// <param name="value">The argument value.</param>
    /// <param name="paramName">The name of the argument.</param>
    /// <returns>The original argument value.</returns>
    /// <exception cref="ArgumentNullException">The argument was <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The argument was an empty string.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string NullOrEmpty([NotNull] string? value, [CallerArgumentExpression("value")] string? paramName = default)
        => Null(value, paramName) == string.Empty ? throw new ArgumentException("Value cannot be an empty string.", paramName) : value;

    /// <summary>
    /// Validates the argument is in the specified range.
    /// </summary>
    /// <typeparam name="T">The argument type.</typeparam>
    /// <param name="value">The argument value.</param>
    /// <param name="minimum">The minimum acceptable value.</param>
    /// <param name="maximum">The maximum acceptable value.</param>
    /// <param name="paramName">The name of the argument.</param>
    /// <returns>The original argument value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The argument was not in the specified range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T OutOfRange<T>(T value, T minimum, T maximum, [CallerArgumentExpression("value")] string? paramName = default)
        where T : IComparable<T>
    {
        var comparer = Comparer<T>.Default;
        if (comparer.Compare(value, minimum) < 0 || comparer.Compare(value, maximum) > 0)
        {
            throw new ArgumentOutOfRangeException(paramName);
        }

        return value;
    }
}
