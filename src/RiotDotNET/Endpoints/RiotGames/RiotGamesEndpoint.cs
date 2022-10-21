namespace RiotDotNET.Endpoints.RiotGames;
using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Services.Riot;
using RiotDotNET.Utilities;

public abstract class RiotGamesEndpoint : EndpointBase
{
    private RiotApiOptions config;

    /// <param name="options">The riot api config.</param>
    protected RiotGamesEndpoint(IHttpClientFactory httpClientFactory, IOptions<RiotApiOptions> options)
        : base(httpClientFactory)
    {
        config = options.Value;
        Argument.NotNull(config.ApiKey, nameof(config.ApiKey), "A valid api key must be specified.");
        Headers.Add(Default.RiotApi.TokenHeader, config.ApiKey);
    }

    /// <inheritdoc/>
    protected override string Host => $"{Route}.{Default.Host.RiotGamesApi}";

    /// <summary>
    /// The api route to prepend the host with.
    /// </summary>
    protected abstract string Route { get; }
}
