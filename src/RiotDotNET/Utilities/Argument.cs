namespace RiotDotNET.Utilities;
using System;

public static class Argument
{
    /// <summary>
    /// Asserts that an argument input is not null. Throws an exception if it is.
    /// </summary>
    /// <param name="input">The input to assert.</param>
    /// <param name="argumentName">The name of the argument.</param>
    /// <param name="message">The optional message to display in the error.</param>
    /// <exception cref="ArgumentNullException">Throws if the input is null.</exception>
    public static void NotNull(string? input, string argumentName, string? message = null)
    {
        if (input == null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
    }
}
