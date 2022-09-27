namespace RiotNET.Services.Riot;

using RiotNET.Interfaces.Services;
using Microsoft.Extensions.Options;

/// <summary>
/// Provides an implementation for accessing data from the Riot Games API.
/// Reference: https://developer.riotgames.com/docs/portal
/// </summary>
public class RiotApi : IRiotApi
{
    private readonly RiotApiOptions config;

    /// <summary>
    /// Initializes a new instance of the <see cref="RiotApi"/> class.
    /// </summary>
    /// <param name="options">The configuration options for the riot api service.</param>
    public RiotApi(IOptions<RiotApiOptions> options)
    {
        config = options.Value ?? new();
    }
}
