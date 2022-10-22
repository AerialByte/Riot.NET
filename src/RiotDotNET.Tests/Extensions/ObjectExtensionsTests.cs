namespace RiotDotNET.Tests.Extensions;
using RiotDotNET.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Enums;

[TestClass]
public class ObjectExtensionsTests
{
    [TestMethod]
    [DataRow("", "")]
    [DataRow("TEST", "test")]
    [DataRow(123, "123")]
    [DataRow(" Test .,! ", " test .,! ")]
    [DataRow(true, "true")]
    [DataRow(false, "false")]
    [DataRow(0.1519, "0.1519")]
    [DataRow(PlatformRoute.BR1, "br1")]
    [DataRow(PlatformRoute.EUN1, "eun1")]
    [DataRow(PlatformRoute.EUW1, "euw1")]
    [DataRow(PlatformRoute.JP1, "jp1")]
    [DataRow(PlatformRoute.KR, "kr")]
    [DataRow(PlatformRoute.LA1, "la1")]
    [DataRow(PlatformRoute.LA2, "la2")]
    [DataRow(PlatformRoute.NA1, "na1")]
    [DataRow(PlatformRoute.OC1, "oc1")]
    [DataRow(PlatformRoute.TR1, "tr1")]
    [DataRow(PlatformRoute.RU, "ru")]
    [DataRow(RegionRoute.AMERICAS, "americas")]
    [DataRow(RegionRoute.ASIA, "asia")]
    [DataRow(RegionRoute.EUROPE, "europe")]
    [DataRow(RegionRoute.SEA, "sea")]
    public void ToStringLowerBasicTest(object input, string expected) => Assert.AreEqual(expected, input.ToStringLower());

    [TestMethod]
    public void ToStringLowerAdvancedTest()
    {
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