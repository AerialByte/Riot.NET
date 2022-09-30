namespace RiotDotNET.Extensions.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;

[TestClass]
public class HttpRequestHeadersExtensionsTests
{
    [TestMethod]
    public void AddTest()
    {
        Dictionary<string, string> headerValues = new()
        {
            ["ABC"] = "123",
            ["DEF"] = "456",
        };

        using var request = new HttpRequestMessage();
        foreach ( var value in headerValues)
        {
            request.Headers.Add(value);
        }

        var result = request.Headers.ToDictionary(x => x.Key, x => x.Value.Single());
        CollectionAssert.AreEqual(headerValues, result);
    }
}