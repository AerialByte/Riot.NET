namespace RiotDotNETTests.Endpoints.RiotGames.Riot;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Endpoints.DTO;
using RiotDotNET.Enums;
using RiotDotNET.Services.Riot;
using RiotDotNETTests.TestHelpers;

[TestClass]
public class AccountEndpointTests : EndpointTestBase
{
    public AccountEndpointTests()
        : base("riot/account/v1/accounts")
    {
    }

    [TestMethod]
    [DataRow(RegionRoute.AMERICAS, "lanegg", "NA1")]
    [DataRow(RegionRoute.AMERICAS, "COOKIEMONSTER123", "NA1")]
    [DataRow(RegionRoute.EUROPE, "Agurin", "EUW")]
    public async Task AccountApiTest(RegionRoute route, string name, string tagLine)
    {
        var puuid = Summoners[name].Puuid;
        var region = Region.FromRoute(route);
        var expected = new AccountDto
        {
            Name = name,
            Puuid = puuid,
            Tag = tagLine
        };

        await ValidateRequest(RiotApi.Account.ByPuuid(puuid, region), region, $"by-puuid/{puuid}", expected);
        await ValidateRequest(RiotApi.Account.ByRiotId(name, tagLine, region), region, $"by-riot-id/{name}/{tagLine}", expected);
    }
}