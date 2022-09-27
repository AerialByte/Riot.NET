namespace RiotNET.Global; 

using RiotNET.Enums;
using RiotNET.Interfaces;
using System.Collections.ObjectModel;

/// <summary>
/// Defines the various riot api server regions.
/// </summary>
public sealed class Region : IHost
{
    /// <summary>
    /// [AMERICAS] Americas: americas.api.riotgames.com
    /// </summary>
    public static Region Americas { get; } = new(RegionId.AMERICAS, "Americas");

    /// <summary>
    /// [ASIA] Asia: asia.api.riotgames.com
    /// </summary>
    public static Region Asia { get; } = new(RegionId.ASIA, "Asia");

    /// <summary>
    /// [EUROPE] Europe: europe.api.riotgames.com
    /// </summary>
    public static Region Europe { get; } = new(RegionId.EUROPE, "Europe");

    /// <summary>
    /// [SEA] Southeast Asia: sea.api.riotgames.com
    /// </summary>
    public static Region SoutheastAsia { get; } = new(RegionId.SEA, "Southeast Asia");

    /// <summary>
    /// An array of available regions.
    /// </summary>
    public static Region[] All { get; } = new[] { Americas, Asia, Europe, SoutheastAsia };

    /// <summary>
    /// A lookup for all of the regions by their assigned <see cref="RegionId"/>.
    /// </summary>
    public static ReadOnlyDictionary<RegionId, Region> ById { get; } = new(All.ToDictionary(x => x.Id, _ => _));

    /// <summary>
    /// A lookup for all of the regions by their defined <see cref="Name"/>.
    /// </summary>
    public static ReadOnlyDictionary<string, Region> ByName { get; } = new(All.ToDictionary(x => x.Name, _ => _));

    private Region(RegionId regionId, string name, string? hostPrefix = null, string riotApiHost = HostDefaults.RiotGamesApi)
    {
        Id = regionId;
        Name = name;
        Host = $"{hostPrefix ?? Id.ToString().ToLower()}.{riotApiHost}";
    }

    /// <summary>
    /// The unique identifier for the region.
    /// </summary>
    public RegionId Id { get; }

    /// <summary>
    /// The display for the region.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The full host for connections to the riot api with this region.
    /// </summary>
    public string Host { get; }
}
