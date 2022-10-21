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

public abstract class EndpointTestBase
{
    private readonly string basePath;

    protected EndpointTestBase(string basePath)
    {
        this.basePath = basePath.Trim('/') + '/';

        Config = Options.Create(new RiotApiOptions { ApiKey = EndpointConstants.TestRiotToken });
        RiotApi = new(TestHttpClientFactory.Default, Config);
    }

    protected Dictionary<string, string> Puuids { get; } = new()
    {
        ["lanegg"] = "htFRLCIeL_WawV6gibBpqNsVlehYPrCJ0mPtJhQFg6Dj2gAFXWKxCGM30OipY2C-bGxuE-Pcs4gDHA",
        ["COOKIEMONSTER123"] = "aYyBaBV3wNx2I6fjnPl8FuG46ZvSvLamKgEtZY2T17J1yJKP96uvyFEn0EfKyxL5bWnHeV5GtYBR1w",
        ["Agurin"] = "i0OCX0PgAEJA7zOUcSQBHoOWcbuSUAnVgnUR0RMMHKGOCYq2RUSaJ9Fp2xe8YzHngrtD_k_cSHVQHQ",
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
