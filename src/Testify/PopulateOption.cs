namespace Testify;

/// <summary>
/// Populate options that can be applied when creating anonymous data.
/// </summary>
public enum PopulateOption
{
    /// <summary>
    /// Do not populate the properties when creating anonymous data.
    /// </summary>
    None,

    /// <summary>
    /// Shallowly populate the properties when creating anonymous data.
    /// </summary>
    Shallow,

    /// <summary>
    /// Deeply populate the properties when creating anonymous data.
    /// </summary>
    Deep,
}
