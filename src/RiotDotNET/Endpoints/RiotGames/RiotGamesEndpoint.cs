namespace RiotDotNET.Endpoints.RiotGames;
using Microsoft.Extensions.Options;
using RiotDotNET.Constants;
using RiotDotNET.Services.Riot;

internal abstract class RiotGamesEndpoint : EndpointBase
{
    private readonly RiotApiOptions config;

    /// <param name="options">The riot api config.</param>
    protected RiotGamesEndpoint(IOptions<RiotApiOptions> options)
    {
        config = options.Value;

        if (string.IsNullOrWhiteSpace(config.ApiKey))
        {
            throw new ArgumentNullException("A valid api key must be specified.", nameof(config.ApiKey));
        }

        Headers.Add(Default.RiotApi.TokenHeader, config.ApiKey);
    }

    /// <inheritdoc/>
    protected override string Host => $"{Route}.{Default.Host.RiotGamesApi}";

    /// <summary>
    /// The api route to prepend the host with.
    /// </summary>
    protected abstract string Route { get; }
}
