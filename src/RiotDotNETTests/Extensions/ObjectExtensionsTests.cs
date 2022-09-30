namespace RiotDotNET.Extensions.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ObjectExtensionsTests
{
    [TestMethod]
    public void ToStringLowerTest()
    {
        Dictionary<object, string?> tests = new()
        {
            [""] = "",
            [" Test .,! "] = " test .,! ",
            ["TEST"] = "test",
            [123] = "123",
        };
        foreach (var inputExpected in tests)
        {
            Assert.AreEqual(inputExpected.Value, inputExpected.Key.ToStringLower());
        }

        Assert.AreEqual(" - (test): custom object! ", new CustomObject { Name = "tEsT", Value = "CuSTOM oBJEcT! " }.ToStringLower());
        Assert.AreEqual(null, new NullToString().ToStringLower());

        object? nullObj = null;
        Assert.ThrowsException<NullReferenceException>(() => nullObj.ToStringLower());
    }

    class CustomObject
    {
        public string Name { get; init; } = default!;
        public string Value { get; init; } = default!;
        public override string ToString() => $" - ({Name}): {Value}";
    }

    class NullToString
    {
        public override string? ToString() => null;
    }
}