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
    /// <returns>The lowercase string, or null if one was not specified.</returns>
    public static string? ToStringLower([NotNull] this object input) => input.ToString()?.ToLower();
}
