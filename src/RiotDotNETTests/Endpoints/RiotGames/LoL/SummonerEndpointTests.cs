namespace RiotDotNETTests.Endpoints.RiotGames.LoL;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Enums;
using RiotDotNET.Extensions;
using RiotDotNET.Services.Riot;
using RiotDotNETTests.Endpoints;
using RiotDotNETTests.TestHelpers;
using System.Net;

[TestClass]
public class SummonerEndpointTests : EndpointTestBase
{
    public SummonerEndpointTests()
        : base("lol/summoner/v4/summoners")
    {
    }

    [TestMethod]
    [DataRow(PlatformRoute.NA1, "lanegg")]
    [DataRow(PlatformRoute.NA1, "COOKIEMONSTER123")]
    [DataRow(PlatformRoute.EUW1, "Agurin")]
    public async Task ByPuuidTests(PlatformRoute route, string name)
    {
        var puuid = Puuids[name];

        var request = RiotApi.Summoner.ByPuuid(puuid, Platform.FromRoute(route));
        var expectedEndpoint = GetUri(route, $"by-puuid/{puuid}");
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        var response = await request.GetReponseAsync();
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Object);
        Assert.AreEqual(puuid, response.Object.Puuid);
        Assert.AreEqual(name, response.Object.Name);
    }
}