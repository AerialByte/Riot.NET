namespace RiotDotNET.Extensions.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class IEnumerableExtensionsTests
{
    [TestMethod]
    public void ForEachTest()
    {
        var insertValues = new[] { 1, 2, 3 };
        var target = new List<int>();
        insertValues.ForEach(x => target.Add(x));
        CollectionAssert.AreEqual(insertValues, target);
    }
}