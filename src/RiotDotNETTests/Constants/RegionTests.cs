namespace RiotDotNET.Constants.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Enums;

[TestClass]
public class RegionTests
{
    [TestMethod]
    public void RegionIntegrity()
    {
        var expectedRegions = typeof(Region).GetProperties().Where(x => x.PropertyType == typeof(Region)).Select(x => x.GetValue(null, null)).ToArray();
        CollectionAssert.AreEqual(expectedRegions, Region.All);

        foreach (var region in Region.All)
        {
            Assert.IsNotNull(region.Name);
            Assert.IsNotNull(region.Route);
            Assert.AreEqual(region, Region.FromRoute(region.Route));
        }

        foreach (var route in Enum.GetValues<RegionRoute>())
        {
            Assert.IsNotNull(Region.FromRoute(route));
        }

        Assert.ThrowsException<ArgumentException>(() => Region.FromRoute((RegionRoute)999));
    }
}