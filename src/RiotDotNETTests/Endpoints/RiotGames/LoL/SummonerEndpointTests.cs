namespace RiotDotNET.Endpoints.RiotGames.LoL.Tests;

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
public class SummonerEndpointTests
{
    const string summonersEndpoint = "https://{platform}.api.riotgames.com/lol/summoner/v4/summoners";

    private readonly IOptions<RiotApiOptions> options = Options.Create(new RiotApiOptions { ApiKey = EndpointConstants.TestRiotToken });

    private const string puuid_lanegg = "htFRLCIeL_WawV6gibBpqNsVlehYPrCJ0mPtJhQFg6Dj2gAFXWKxCGM30OipY2C-bGxuE-Pcs4gDHA";
    private const string puuid_COOKIEMONSTER123 = "aYyBaBV3wNx2I6fjnPl8FuG46ZvSvLamKgEtZY2T17J1yJKP96uvyFEn0EfKyxL5bWnHeV5GtYBR1w";
    private const string puuid_Agurin = "i0OCX0PgAEJA7zOUcSQBHoOWcbuSUAnVgnUR0RMMHKGOCYq2RUSaJ9Fp2xe8YzHngrtD_k_cSHVQHQ";

    [TestMethod]
    [DataRow(PlatformRoute.NA1, puuid_lanegg, $"{summonersEndpoint}/by-puuid/{puuid_lanegg}", "lanegg")]
    [DataRow(PlatformRoute.NA1, puuid_COOKIEMONSTER123, $"{summonersEndpoint}/by-puuid/{puuid_COOKIEMONSTER123}", "COOKIEMONSTER123")]
    [DataRow(PlatformRoute.EUW1, puuid_Agurin, $"{summonersEndpoint}/by-puuid/{puuid_Agurin}", "Agurin")]
    public async Task ByPuuidTests(PlatformRoute route, string puuid, string expectedEndpoint, string expectedName)
    {
        expectedEndpoint = expectedEndpoint.Replace("{platform}", route.ToStringLower());

        var riotApi = new RiotApi(TestHttpClientFactory.Default, options);
        var request = riotApi.Summoner.ByPuuid(puuid, Platform.FromRoute(route));
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        var response = await request.GetReponseAsync();
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Object);
        Assert.AreEqual(puuid, response.Object.Puuid);
        Assert.AreEqual(expectedName, response.Object.Name);
    }
}