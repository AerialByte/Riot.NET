namespace RiotDotNETTests.Endpoints.RiotGames.LoL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Enums;
using RiotDotNET.Services.Riot;
using RiotDotNETTests.TestHelpers;

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
        var expected = Summoners[name];
        var platform = Platform.FromRoute(route);
        string accountId = expected.AccountId,
            summonerId = expected.Id,
            puuid = expected.Puuid;

        await ValidateRequest(RiotApi.Summoner.ByAccount(accountId, platform), platform, $"by-account/{accountId}", expected);
        await ValidateRequest(RiotApi.Summoner.ById(summonerId, platform), platform, $"{summonerId}", expected);
        await ValidateRequest(RiotApi.Summoner.ByName(name, platform), platform, $"by-name/{name}", expected);
        await ValidateRequest(RiotApi.Summoner.ByPuuid(puuid, platform), platform, $"by-puuid/{puuid}", expected);
    }
}