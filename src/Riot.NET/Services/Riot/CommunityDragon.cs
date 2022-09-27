namespace RiotNET.Services.Riot;

using Microsoft.Extensions.Options;
using RiotNET.Interfaces.Services;

/// <summary>
/// Provides an implementation for accessing data from Community Dragon.
/// Reference: https://www.communitydragon.org/
/// </summary>
public sealed class CommunityDragon : ICommunityDragon
{
    private readonly CommunityDragonOptions config;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommunityDragon"/> class.
    /// </summary>
    /// <param name="options">The configuration options for the community dragon service.</param>
    public CommunityDragon(IOptions<CommunityDragonOptions> options)
    {
        config = options.Value ?? new();
    }
}
