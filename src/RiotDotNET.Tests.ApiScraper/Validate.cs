namespace RiotDotNET.Tests.ApiScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
