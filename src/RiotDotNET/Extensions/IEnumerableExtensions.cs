namespace RiotDotNET.Extensions;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Performs the specified action on each input item in the <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the enumerable data.</typeparam>
    /// <param name="input">The input enumerable.</param>
    /// <param name="action">The action to perform on each item.</param>
    public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
    {
        foreach (var item in input)
        {
            action(item);
        }
    }
}
