namespace RiotDotNET.Extensions.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ObjectExtensionsTests
{
    [TestMethod]
    [DataRow(null, null)]
    [DataRow("", "")]
    [DataRow(" ", " ")]
    [DataRow("TEST", "test")]
    [DataRow(123, "123")]
    public void ToStringLowerTest(object input, string expectedOutput) =>
        Assert.AreEqual(expectedOutput, input.ToStringLower());
}