using RiotDotNET.Extensions;

namespace RiotDotNETTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

[TestClass]
public class DictionaryExtensionsTests
{
    [TestMethod]
    public void ReadOnlyTest()
    {
        object test = new();
        var dictionary = new Dictionary<string, object>()
        {
            ["test"] = test
        };

        IDictionary<string, object> readOnly = dictionary.ReadOnly();
        Assert.IsNotNull(readOnly);
        Assert.IsInstanceOfType(readOnly, typeof(ReadOnlyDictionary<string, object>));
        Assert.AreEqual(test, readOnly["test"]);
        Assert.ThrowsException<NotSupportedException>(() => readOnly["test"] = new());
    }
}