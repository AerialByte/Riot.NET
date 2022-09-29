namespace RiotDotNET.Constants;
using RiotDotNET.Enums;
using System.Collections.ObjectModel;

/// <summary>
/// Defines the various riot api server regions.
/// </summary>
public sealed class Region
{
    /// <summary>
    /// [AMERICAS] Americas: americas.api.riotgames.com
    /// </summary>
    public static Region Americas { get; } = new(RegionRoute.AMERICAS, "Americas");

    /// <summary>
    /// [ASIA] Asia: asia.api.riotgames.com
    /// </summary>
    public static Region Asia { get; } = new(RegionRoute.ASIA, "Asia");

    /// <summary>
    /// [EUROPE] Europe: europe.api.riotgames.com
    /// </summary>
    public static Region Europe { get; } = new(RegionRoute.EUROPE, "Europe");

    /// <summary>
    /// [SEA] Southeast Asia: sea.api.riotgames.com
    /// </summary>
    public static Region SoutheastAsia { get; } = new(RegionRoute.SEA, "Southeast Asia");

    /// <summary>
    /// An array of available regions.
    /// </summary>
    public static Region[] All { get; } = new[] { Americas, Asia, Europe, SoutheastAsia };

    /// <summary>
    /// A lookup for all of the regions by their assigned <see cref="RegionRoute"/>.
    /// </summary>
    public static ReadOnlyDictionary<RegionRoute, Region> RouteLookup { get; } = new(All.ToDictionary(x => x.Route, _ => _));

    private Region(RegionRoute regionRoute, string name)
    {
        Route = regionRoute;
        Name = name;
    }

    /// <summary>
    /// The unique identifier for the region.
    /// </summary>
    public RegionRoute Route { get; }

    /// <summary>
    /// The display for the region.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Attempts to get the region by route.
    /// </summary>
    /// <param name="regionRoute">The unique route for the region.</param>
    /// <returns>The region associated with the specified <see cref="RegionRoute"/>.</returns>
    /// <exception cref="ArgumentException">Throws if the <see cref="RegionRoute"/> specified was invalid.</exception>
    public static Region FromRoute(RegionRoute regionRoute) => RouteLookup.TryGetValue(regionRoute, out var region) ? region
        : throw new ArgumentException("Invalid region id specified.", nameof(regionRoute));
}
