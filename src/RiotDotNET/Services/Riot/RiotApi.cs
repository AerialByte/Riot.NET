namespace RiotDotNET.Services.Riot;

using Microsoft.Extensions.Options;
using RiotDotNET.Endpoints.Interfaces;
using RiotDotNET.Endpoints.RiotGames.LoL;
using RiotDotNET.Endpoints.RiotGames.Riot;

/// <summary>
/// Provides an implementation for accessing data from the Riot Games API.
/// Reference: https://developer.riotgames.com/docs/portal
/// </summary>
public class RiotApi : IRiotApi
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IOptions<RiotApiOptions> options;

    /// <summary>
    /// Initializes a new instance of the <see cref="RiotApi"/> class.
    /// </summary>
    /// <param name="options">The configuration config for the riot api service.</param>
    public RiotApi(IHttpClientFactory httpClientFactory, IOptions<RiotApiOptions> options)
    {
        this.httpClientFactory = httpClientFactory;
        this.options = options;
    }

    /// <inheritdoc/>
    public IAccountEndpoint Account => new AccountEndpoint(httpClientFactory, options);

    /// <inheritdoc/>
    public ISummonerEndpoint Summoner => new SummonerEndpoint(httpClientFactory, options);
}
