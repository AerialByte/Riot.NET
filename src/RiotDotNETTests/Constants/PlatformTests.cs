namespace RiotDotNETTests.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Enums;

[TestClass]
public class PlatformTests
{
    [TestMethod]
    public void PlatformIntegrity()
    {
        var expectedPlatforms = typeof(Platform).GetProperties().Where(x => x.PropertyType == typeof(Platform)).Select(x => x.GetValue(null, null)).ToArray();
        CollectionAssert.AreEqual(expectedPlatforms, Platform.All);

        foreach (var platform in Platform.All)
        {
            Assert.IsNotNull(platform.Name);
            Assert.IsNotNull(platform.Route);
            Assert.IsNotNull(platform.Code);
            Assert.AreEqual(platform, Platform.FromCode(platform.Code));
            Assert.AreEqual(platform, Platform.FromRoute(platform.Route));
        }

        foreach (var code in Enum.GetValues<PlatformCode>())
        {
            Assert.IsNotNull(Platform.FromCode(code));
        }

        foreach (var route in Enum.GetValues<PlatformRoute>())
        {
            Assert.IsNotNull(Platform.FromRoute(route));
        }

        Assert.ThrowsException<ArgumentException>(() => Platform.FromCode((PlatformCode)999));
        Assert.ThrowsException<ArgumentException>(() => Platform.FromRoute((PlatformRoute)999));
    }
}