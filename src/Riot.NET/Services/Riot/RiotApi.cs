namespace RiotNET.Services.Riot;
using Microsoft.Extensions.Options;
using RiotNET.Endpoints.RiotGames.Riot;

/// <summary>
/// Provides an implementation for accessing data from the Riot Games API.
/// Reference: https://developer.riotgames.com/docs/portal
/// </summary>
public class RiotApi : IRiotApi
{
    private readonly IOptions<RiotApiOptions> options;

    /// <summary>
    /// Initializes a new instance of the <see cref="RiotApi"/> class.
    /// </summary>
    /// <param name="options">The configuration config for the riot api service.</param>
    public RiotApi(IOptions<RiotApiOptions> options)
    {
        this.options = options;
    }

    /// <inheritdoc/>
    public IAccountEndpoint Account => new AccountEndpoint(options);
}
