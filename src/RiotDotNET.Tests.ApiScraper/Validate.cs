namespace RiotDotNET.Tests.ApiScraper;
using System;

public static class Validate
{
    public static void NotNull(object? input, string? inputName = null, string? message = null)
    {
        if (input == null)
        {
            throw new ArgumentNullException(inputName, message);
        }
    }
}
