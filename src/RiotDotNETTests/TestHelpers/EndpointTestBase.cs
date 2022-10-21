namespace RiotDotNETTests.TestHelpers;

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiotDotNET.Constants;
using RiotDotNET.Endpoints.RiotGames.Riot;
using RiotDotNET.Endpoints;
using RiotDotNET.Enums;
using RiotDotNET.Extensions;
using RiotDotNET.Services.Riot;
using RiotDotNETTests.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using RiotDotNET.Endpoints.RiotGames.LoL;

public abstract class EndpointTestBase
{
    private readonly string basePath;

    protected EndpointTestBase(string basePath)
    {
        this.basePath = basePath.Trim('/') + '/';

        Config = Options.Create(new RiotApiOptions { ApiKey = EndpointConstants.TestRiotToken });
        RiotApi = new(TestHttpClientFactory.Default, Config);
    }

    protected Dictionary<string, SummonerDto> Summoners { get; } = new()
    {
        ["Agurin"] = new()
        {
            Id = "oTWjkY-F-oLAqHs5qkM54UnvxS8cpAewKAv9swy7oKSzh0c",
            AccountId = "B54j00MSC0onav0-DCe6adKarIn6ALIHeyw9Bxv3CicTiQ",
            Puuid = "UNyvfnTchNzhdHA9Y_PAegVcaUOy0BaGY60bhT9vVgOmoOohUkmEBywVZiMCuRrB_lN14JgjHBsgaw",
            Name = "Agurin",
            ProfileIconId = 4353,
            RevisionDateMilliseconds = 1666110833000,
            Level = 791,
        },
        ["lanegg"] = new()
        {
            Id = "VN_zm246D0g0nbmtroeam98OhX8LwYLlrhy07FAjmCB3yUu3GIdN22QCCQ",
            AccountId = "yKz49zfgnkVTEYG7-rPJZr60B7p_-Ek5Tcjb1_4niaCEO_rDUwGqS8DW",
            Puuid = "ykDllSHvCjx0dPvV_sQDBdgjoylk7OPGxPdt49pk6vox2nZ2AvRl9cGQHDKP6Uvtgm8J2KTZHxF4YQ",
            Name = "lanegg",
            ProfileIconId = 5067,
            RevisionDateMilliseconds = 1635387842000,
            Level = 29,
        },
        ["COOKIEMONSTER123"] = new()
        {
            Id = "4Vaug7PDl8Rt8nZxYrEciphqzMiUTai-q3SW6hzjGXi8VK4IulsiAIvQ8w",
            AccountId = "TBJXabWboLPseVmGoS6oyqs_TrqkwW3LNhMdj4z5-_a0_g-SpgM-IFEG",
            Puuid = "q-59OC-fr19MM_ev8zX7SEkAe_5DK6xID7Lbri_51iqEKN5w1vbgrHXeuxPeBvg96A2XkpBPqZ-2TQ",
            Name = "COOKIEMONSTER123",
            ProfileIconId = 5067,
            RevisionDateMilliseconds = 1665962308000,
            Level = 214,
        },
    };

    protected IOptions<RiotApiOptions> Config { get; }

    protected RiotApi RiotApi { get; }

    protected string GetUri(PlatformRoute route, string path) => GetUri(route.ToStringLower()!, path);

    protected string GetUri(RegionRoute route, string path) => GetUri(route.ToStringLower()!, path);

    private string GetUri(string route, string path)
    {
        var routeUri = new UriBuilder(Uri.UriSchemeHttps, $"{route}.api.riotgames.com") { Path = basePath }.Uri;
        var retUri = new Uri(routeUri, path.Trim('/'));
        return retUri.AbsoluteUri;
    }

    protected async Task ValidateRequest<TDto>(EndpointRequest<TDto> request, Region region, string path, TDto expectedResponseObject)
        where TDto : class
    {
        var expectedEndpoint = GetUri(region.Route, path);
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        await ValidateRequest(request, expectedResponseObject);
    }

    protected async Task ValidateRequest<TDto>(EndpointRequest<TDto> request, Platform platform, string path, TDto expectedResponseObject)
        where TDto : class
    {
        var expectedEndpoint = GetUri(platform.Route, path);
        Assert.AreEqual(expectedEndpoint, request.Uri.AbsoluteUri);

        await ValidateRequest(request, expectedResponseObject);
    }

    private static async Task ValidateRequest<TDto>(EndpointRequest<TDto> request, TDto expectedResponseObject)
        where TDto : class
    {
        var response = await request.GetReponseAsync();
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Object);

        foreach (var prop in typeof(TDto).GetProperties().Where(p => p.CustomAttributes.OfType<JsonPropertyNameAttribute>().Any()))
        {
            var expected = prop.GetValue(expectedResponseObject);
            var actual = prop.GetValue(response.Object);
            Assert.AreEqual(expected, actual);
        }
    }
}
