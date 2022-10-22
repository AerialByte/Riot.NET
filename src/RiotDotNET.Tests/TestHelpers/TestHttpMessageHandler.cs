namespace RiotDotNET.Tests.TestHelpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Tests.Endpoints;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class TestHttpMessageHandler : HttpMessageHandler
{
    private static Dictionary<HttpStatusCode, string> invalidResponse = new()
    {
        [HttpStatusCode.BadRequest] = @"{""status"":{""status_code"":400}}",
        [HttpStatusCode.Unauthorized] = @"{""status"":{""message"":""Unauthorized"",""status_code"":401}}",
        [HttpStatusCode.NotFound] = @"{""status"":{""message"":""Data not found"",""status_code"":404}}",
    };

    const string accountV1Endpoint = "/riot/account/v1";
    const string championMasteryV4Endpoint = "/lol/champion-mastery/v4";
    const string championV3Endpoint = "/lol/platform/v3";
    const string clashV1Endpoint = "/lol/clash/v1";
    const string leagueExpV4Endpoint = "/lol/league-exp/v4";
    const string leagueV4Endpoint = "/lol/league/v4";
    const string lolChallengesV1Endpoint = "/lol/challenges/v1";
    const string summonerV4Endpoint = "/lol/summoner/v4";

    private static readonly Dictionary<string, Dictionary<string, string>> responses = new()
    {
        #region Account-V1 Responses

        [accountV1Endpoint] = new()
        {
            ["/accounts/by-riot-id/Agurin/EUW"] = @"{
    ""puuid"": ""UNyvfnTchNzhdHA9Y_PAegVcaUOy0BaGY60bhT9vVgOmoOohUkmEBywVZiMCuRrB_lN14JgjHBsgaw"",
    ""gameName"": ""Agurin"",
    ""tagLine"": ""EUW""
}",

            ["/accounts/by-riot-id/lanegg/NA1"] = @"{
    ""puuid"": ""ykDllSHvCjx0dPvV_sQDBdgjoylk7OPGxPdt49pk6vox2nZ2AvRl9cGQHDKP6Uvtgm8J2KTZHxF4YQ"",
    ""gameName"": ""lanegg"",
    ""tagLine"": ""NA1""
}",

            ["/accounts/by-riot-id/COOKIEMONSTER123/NA1"] = @"{
    ""puuid"": ""q-59OC-fr19MM_ev8zX7SEkAe_5DK6xID7Lbri_51iqEKN5w1vbgrHXeuxPeBvg96A2XkpBPqZ-2TQ"",
    ""gameName"": ""COOKIEMONSTER123"",
    ""tagLine"": ""NA1""
}",
        },

        #endregion

        #region Summoner-V4 Responses

        [summonerV4Endpoint] = new()
        {
            ["/summoners/by-name/Agurin"] = @"{
    ""id"": ""oTWjkY-F-oLAqHs5qkM54UnvxS8cpAewKAv9swy7oKSzh0c"",
    ""accountId"": ""B54j00MSC0onav0-DCe6adKarIn6ALIHeyw9Bxv3CicTiQ"",
    ""puuid"": ""UNyvfnTchNzhdHA9Y_PAegVcaUOy0BaGY60bhT9vVgOmoOohUkmEBywVZiMCuRrB_lN14JgjHBsgaw"",
    ""name"": ""Agurin"",
    ""profileIconId"": 4353,
    ""revisionDate"": 1666110833000,
    ""summonerLevel"": 791
}",

            ["/summoners/by-name/lanegg"] = @"{
    ""id"": ""VN_zm246D0g0nbmtroeam98OhX8LwYLlrhy07FAjmCB3yUu3GIdN22QCCQ"",
    ""accountId"": ""yKz49zfgnkVTEYG7-rPJZr60B7p_-Ek5Tcjb1_4niaCEO_rDUwGqS8DW"",
    ""puuid"": ""ykDllSHvCjx0dPvV_sQDBdgjoylk7OPGxPdt49pk6vox2nZ2AvRl9cGQHDKP6Uvtgm8J2KTZHxF4YQ"",
    ""name"": ""lanegg"",
    ""profileIconId"": 5067,
    ""revisionDate"": 1635387842000,
    ""summonerLevel"": 29
}",

            ["/summoners/by-name/COOKIEMONSTER123"] = @"{
    ""id"": ""4Vaug7PDl8Rt8nZxYrEciphqzMiUTai-q3SW6hzjGXi8VK4IulsiAIvQ8w"",
    ""accountId"": ""TBJXabWboLPseVmGoS6oyqs_TrqkwW3LNhMdj4z5-_a0_g-SpgM-IFEG"",
    ""puuid"": ""q-59OC-fr19MM_ev8zX7SEkAe_5DK6xID7Lbri_51iqEKN5w1vbgrHXeuxPeBvg96A2XkpBPqZ-2TQ"",
    ""name"": ""COOKIEMONSTER123"",
    ""profileIconId"": 907,
    ""revisionDate"": 1665962308000,
    ""summonerLevel"": 214
}",
        },

        #endregion
    };

    static TestHttpMessageHandler()
    {
        foreach (string accountJson in responses[accountV1Endpoint].Values.ToArray())
        {
            var account = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(accountJson) ?? throw new Exception("Invalid test setup -- HttpMessageHandler json.");
            responses[accountV1Endpoint][$"/accounts/by-puuid/{account["puuid"]}"] = accountJson;
        }

        foreach (string summonerJson in responses[summonerV4Endpoint].Values.ToArray())
        {
            var summoner = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(summonerJson) ?? throw new Exception("Invalid test setup -- HttpMessageHandler json.");
            responses[summonerV4Endpoint][$"/summoners/{summoner["id"]}"] = summonerJson;
            responses[summonerV4Endpoint][$"/summoners/by-account/{summoner["accountId"]}"] = summonerJson;
            responses[summonerV4Endpoint][$"/summoners/by-puuid/{summoner["puuid"]}"] = summonerJson;
        }
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var riotToken = request.Headers.Where(x => x.Key == "X-Riot-Token").Select(x => x.Value?.FirstOrDefault()).FirstOrDefault();
        if (EndpointConstants.TestRiotToken != riotToken)
        {
            return Task.FromResult(Invalid(HttpStatusCode.Unauthorized));
        }

        var path = request?.RequestUri?.PathAndQuery;
        Assert.IsNotNull(path);

        foreach (var endpoint in new[] { accountV1Endpoint, summonerV4Endpoint })
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (responses.ContainsKey(endpoint) && path.StartsWith(endpoint))
            {
                var key = path.Replace(endpoint, "");
                if (responses[endpoint].ContainsKey(key))
                {
                    return Task.FromResult(StringResponse(responses[endpoint][key]));
                }
            }
        }

        return Task.FromResult(Invalid(HttpStatusCode.NotFound));
    }

    private static HttpResponseMessage Invalid(HttpStatusCode code) => StringResponse(invalidResponse[code], code);

    private static HttpResponseMessage StringResponse(string content, HttpStatusCode code = HttpStatusCode.OK) => new(code)
    {
        Content = new StringContent(content)
    };
}
