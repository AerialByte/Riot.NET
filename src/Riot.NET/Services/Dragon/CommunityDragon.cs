namespace RiotNET.Services.Dragon;
using Microsoft.Extensions.Options;

/// <summary>
/// Provides an implementation for accessing data from Community Dragon.
/// Reference: https://www.communitydragon.org/
/// </summary>
public sealed class CommunityDragon : DataDragon, ICommunityDragon
{
    private readonly CommunityDragonOptions config;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommunityDragon"/> class.
    /// </summary>
    /// <param name="options">The configuration config for the community dragon service.</param>
    public CommunityDragon(IOptions<CommunityDragonOptions> options)
        : base(options)
    {
        config = options.Value ?? new();
    }
}
