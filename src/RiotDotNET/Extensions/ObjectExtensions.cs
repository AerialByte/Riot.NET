namespace RiotDotNET.Extensions;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Extension methods for all objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Converts the object to its lowercase string representations by combining <see cref="object.ToString"/> and <see cref="string.ToLower"/>.
    /// </summary>
    /// <param name="input">The input object.</param>
    /// <returns>The lowercase string.</returns>
    /// <exception cref="ArgumentNullException">If the input is null.</exception>
    public static string? ToStringLower<T>([NotNull] this T input) => input!.ToString()?.ToLower();
}
