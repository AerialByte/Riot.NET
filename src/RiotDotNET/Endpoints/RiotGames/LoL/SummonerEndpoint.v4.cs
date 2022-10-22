namespace RiotDotNET.Endpoints.RiotGames.LoL;

using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Endpoints.DTO;
using RiotDotNET.Endpoints.Interfaces;
using RiotDotNET.Services.Riot;
using System.Net.Http;

/// <inheritdoc cref="ISummonerEndpoint"/>
internal sealed class SummonerEndpoint : PlatformEndpoint, ISummonerEndpoint
{
    /// <inheritdoc cref="PlatformEndpoint(IHttpClientFactory, IOptions{RiotApiOptions})"/>
    internal SummonerEndpoint(IHttpClientFactory httpClientFactory, IOptions<RiotApiOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <inheritdoc/>
    public EndpointRequest<SummonerDto> ByAccount(string accountId, Platform platform) =>
        Request<SummonerDto>(platform, $"{Default.RiotApi.Path.Summoner_v4}/summoners/by-account/{accountId}");

    /// <inheritdoc/>
    public EndpointRequest<SummonerDto> ById(string summonerId, Platform platform) =>
        Request<SummonerDto>(platform, $"{Default.RiotApi.Path.Summoner_v4}/summoners/{summonerId}");

    /// <inheritdoc/>
    public EndpointRequest<SummonerDto> ByName(string name, Platform platform) =>
        Request<SummonerDto>(platform, $"{Default.RiotApi.Path.Summoner_v4}/summoners/by-name/{name}");

    /// <inheritdoc/>
    public EndpointRequest<SummonerDto> ByPuuid(string puuid, Platform platform) =>
        Request<SummonerDto>(platform, $"{Default.RiotApi.Path.Summoner_v4}/summoners/by-puuid/{puuid}");
}
