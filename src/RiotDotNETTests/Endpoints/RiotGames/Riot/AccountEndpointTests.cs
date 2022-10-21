namespace RiotDotNET.Endpoints.RiotGames.Riot.Tests;

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Endpoints.RiotGames.Riot;
using RiotDotNET.Enums;
using RiotDotNET.Extensions;
using RiotDotNET.Services.Riot;
using RiotDotNETTests.Endpoints;
using RiotDotNETTests.TestClient;
using System.Net;

[TestClass]
public class AccountEndpointTests
{
    const string accountsEndpoint = "https://{region}.api.riotgames.com/riot/account/v1/accounts";

    private readonly IOptions<RiotApiOptions> options = Options.Create(new RiotApiOptions { ApiKey = EndpointConstants.TestRiotToken });

    private const string puuid_lanegg = "htFRLCIeL_WawV6gibBpqNsVlehYPrCJ0mPtJhQFg6Dj2gAFXWKxCGM30OipY2C-bGxuE-Pcs4gDHA";
    private const string puuid_COOKIEMONSTER123 = "aYyBaBV3wNx2I6fjnPl8FuG46ZvSvLamKgEtZY2T17J1yJKP96uvyFEn0EfKyxL5bWnHeV5GtYBR1w";
    private const string puuid_Agurin = "i0OCX0PgAEJA7zOUcSQBHoOWcbuSUAnVgnUR0RMMHKGOCYq2RUSaJ9Fp2xe8YzHngrtD_k_cSHVQHQ";

    [TestMethod]
    [DataRow(RegionRoute.AMERICAS, puuid_lanegg, $"{accountsEndpoint}/by-puuid/{puuid_lanegg}", "lanegg", "NA1")]
    [DataRow(RegionRoute.AMERICAS, puuid_COOKIEMONSTER123, $"{accountsEndpoint}/by-puuid/{puuid_COOKIEMONSTER123}", "COOKIEMONSTER123", "NA1")]
    [DataRow(RegionRoute.EUROPE, puuid_Agurin, $"{accountsEndpoint}/by-puuid/{puuid_Agurin}", "Agurin", "EUW")]
    public async Task ByPuuidTests(RegionRoute route, string puuid, string expectedEndpoint, string expectedName, string expectedTag)
    {
        expectedEndpoint = expectedEndpoint.Replace("{region}", route.ToStringLower());

        var request = new AccountEndpoint(TestHttpClientFactory.Default, options).ByPuuid(puuid, Region.FromRoute(route));
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        var response = await request.GetReponseAsync();
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Object);
        Assert.AreEqual(puuid, response.Object.Puuid);
        Assert.AreEqual(expectedName, response.Object.Name);
        Assert.AreEqual(expectedTag, response.Object.Tag);
    }

    [TestMethod]
    [DataRow(RegionRoute.AMERICAS, "lanegg", "NA1", $"{accountsEndpoint}/by-riot-id/lanegg/NA1", puuid_lanegg)]
    [DataRow(RegionRoute.AMERICAS, "COOKIEMONSTER123", "NA1", $"{accountsEndpoint}/by-riot-id/COOKIEMONSTER123/NA1", puuid_COOKIEMONSTER123)]
    [DataRow(RegionRoute.EUROPE, "Agurin", "EUW", $"{accountsEndpoint}/by-riot-id/Agurin/EUW", puuid_Agurin)]
    public async Task ByRiotIdTests(RegionRoute route, string gameName, string tagLine, string expectedEndpoint, string expectedPuuid)
    {
        expectedEndpoint = expectedEndpoint.Replace("{region}", route.ToStringLower());

        var request = new AccountEndpoint(TestHttpClientFactory.Default, options).ByRiotId(gameName, tagLine, Region.FromRoute(route));
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        var response = await request.GetReponseAsync();
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Object);
        Assert.AreEqual(expectedPuuid, response.Object.Puuid);
        Assert.AreEqual(gameName, response.Object.Name);
        Assert.AreEqual(tagLine, response.Object.Tag);
    }
}