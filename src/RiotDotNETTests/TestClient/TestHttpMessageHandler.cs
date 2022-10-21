namespace RiotDotNETTests.TestClient;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNETTests.Endpoints;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

public class TestHttpMessageHandler : HttpMessageHandler
{
    private static Dictionary<HttpStatusCode, string> invalidResponse = new()
    {
        [HttpStatusCode.Unauthorized] = @"{""status"":{""message"":""Unauthorized"",""status_code"":401}}",
        [HttpStatusCode.NotFound] = @"{""status"":{""message"":""Data not found"",""status_code"":404}}",
    };

    const string accountV1Endpoint = "/riot/account/v1/accounts";
    const string summonerV4Endpoint = "/lol/summoner/v4/summoners";

    private static readonly Dictionary<string, Dictionary<string, string>> responses = new()
    {

        #region Account-V1 Responses

        [accountV1Endpoint] = new()
        {
            ["/by-riot-id/Agurin/EUW"] = @"{
    ""puuid"": ""i0OCX0PgAEJA7zOUcSQBHoOWcbuSUAnVgnUR0RMMHKGOCYq2RUSaJ9Fp2xe8YzHngrtD_k_cSHVQHQ"",
    ""gameName"": ""Agurin"",
    ""tagLine"": ""EUW""
}",

            ["/by-riot-id/lanegg/NA1"] = @"{
    ""puuid"": ""htFRLCIeL_WawV6gibBpqNsVlehYPrCJ0mPtJhQFg6Dj2gAFXWKxCGM30OipY2C-bGxuE-Pcs4gDHA"",
    ""gameName"": ""lanegg"",
    ""tagLine"": ""NA1""
}",

            ["/by-riot-id/COOKIEMONSTER123/NA1"] = @"{
    ""puuid"": ""aYyBaBV3wNx2I6fjnPl8FuG46ZvSvLamKgEtZY2T17J1yJKP96uvyFEn0EfKyxL5bWnHeV5GtYBR1w"",
    ""gameName"": ""COOKIEMONSTER123"",
    ""tagLine"": ""NA1""
}",
        },

#endregion

        #region Summoner-V4 Responses

        [summonerV4Endpoint] = new()
        {
            ["/by-name/Agurin"] = @"{
    ""id"": ""ZYrKeyRnPGJBBOctFhnlbYjrsiX5YXDMUlnl7bYEKJI9vOs"",
    ""accountId"": ""19qZwxUmzq5JvjiAy21G5JJ-PLbaeWVrQpIPZBftmwCJ9w"",
    ""puuid"": ""i0OCX0PgAEJA7zOUcSQBHoOWcbuSUAnVgnUR0RMMHKGOCYq2RUSaJ9Fp2xe8YzHngrtD_k_cSHVQHQ"",
    ""name"": ""Agurin"",
    ""profileIconId"": 4353,
    ""revisionDate"": 1666110833000,
    ""summonerLevel"": 791
}",

            ["/by-name/lanegg"] = @"{
    ""id"": ""62y1gjBhMgJsMsVdd-339qibl8UcGot-D7MOQunYS7SYf5amO_j_sN9JMw"",
    ""accountId"": ""xM75th03KhMhabLGYq7emgXTJeMQKHJ5miit9Qt-aESM4ufV17Q5ztkh"",
    ""puuid"": ""htFRLCIeL_WawV6gibBpqNsVlehYPrCJ0mPtJhQFg6Dj2gAFXWKxCGM30OipY2C-bGxuE-Pcs4gDHA"",
    ""name"": ""lanegg"",
    ""profileIconId"": 5067,
    ""revisionDate"": 1635387842000,
    ""summonerLevel"": 29
}",

            ["/by-name/COOKIEMONSTER123"] = @"{
    ""id"": ""nt3lh6DtEiIp41U3-XP5usI_r-m3qrhZPFkMtJG4F-79Yv3AeTGdTJISiQ"",
    ""accountId"": ""rLuWFgWNWx_yo8TRw7vXp_KnswtBMU341TyF2V8VweDiKdwnURI_t5UC"",
    ""puuid"": ""aYyBaBV3wNx2I6fjnPl8FuG46ZvSvLamKgEtZY2T17J1yJKP96uvyFEn0EfKyxL5bWnHeV5GtYBR1w"",
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
            responses[accountV1Endpoint][$"/by-puuid/{account["puuid"]}"] = accountJson;
        }

        foreach (string summonerJson in responses[summonerV4Endpoint].Values.ToArray())
        {
            var summoner = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(summonerJson) ?? throw new Exception("Invalid test setup -- HttpMessageHandler json.");
            responses[summonerV4Endpoint][$"/{summoner["id"]}"] = summonerJson;
            responses[summonerV4Endpoint][$"/by-account/{summoner["accountId"]}"] = summonerJson;
            responses[summonerV4Endpoint][$"/by-puuid/{summoner["puuid"]}"] = summonerJson;
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
