namespace RiotNET.Constants;

using RiotNET.Enums;
using System.Collections.ObjectModel;

/// <summary>
/// Defines the various riot api server platforms.
/// </summary>
public sealed class Platform
{
    /// <summary>
    /// Brazil (BR): br1.api.riotgames.com
    /// </summary>
    public static Platform Brazil { get; } = new(PlatformRoute.BR1, PlatformCode.BR, "Brazil");

    /// <summary>
    /// Europe Nordic & East (EUNE): eun1.api.riotgames.com
    /// </summary>
    public static Platform EuropeNordicEast { get; } = new(PlatformRoute.EUN1, PlatformCode.EUNE, "Europe Nordic & East");

    /// <summary>
    /// Europe West (EUW): euw1.api.riotgames.com
    /// </summary>
    public static Platform EuropeWest { get; } = new(PlatformRoute.EUW1, PlatformCode.EUW, "Europe West");

    /// <summary>
    /// Japan (JP): jp1.api.riotgames.com
    /// </summary>
    public static Platform Japan { get; } = new(PlatformRoute.JP1, PlatformCode.JP, "Japan");

    /// <summary>
    /// Republic of Korea (KR): kr.api.riotgames.com
    /// </summary>
    public static Platform Korea { get; } = new(PlatformRoute.KR, PlatformCode.KR, "Korea");

    /// <summary>
    /// Latin America North (LAN): la1.api.riotgames.com
    /// </summary>
    public static Platform LatinAmericaNorth { get; } = new(PlatformRoute.LA1, PlatformCode.LAN, "Latin America North");

    /// <summary>
    /// Latin America South (LAS): la2.api.riotgames.com
    /// </summary>
    public static Platform LatinAmericaSouth { get; } = new(PlatformRoute.LA2, PlatformCode.LAS, "Latin America South");

    /// <summary>
    /// North America (NA): na1.api.riotgames.com
    /// </summary>
    public static Platform NorthAmerica { get; } = new(PlatformRoute.NA1, PlatformCode.NA, "North America");

    /// <summary>
    /// Oceania (OCE): oc1.api.riotgames.com
    /// </summary>
    public static Platform Oceania { get; } = new(PlatformRoute.OC1, PlatformCode.OCE, "Oceania");

    /// <summary>
    /// Turkey (TR): tr1.api.riotgames.com
    /// </summary>
    public static Platform Turkey { get; } = new(PlatformRoute.TR1, PlatformCode.TR, "Turkey");

    /// <summary>
    /// Russia (RU): ru.api.riotgames.com
    /// </summary>
    public static Platform Russia { get; } = new(PlatformRoute.RU, PlatformCode.RU, "Russia");

    /// <summary>
    /// An array of available platforms.
    /// </summary>
    public static Platform[] All { get; } = new[]
    {
        Brazil,
        EuropeNordicEast,
        EuropeWest,
        Japan,
        Korea,
        LatinAmericaNorth,
        LatinAmericaSouth,
        NorthAmerica,
        Oceania,
        Turkey,
        Russia
    };

    /// <summary>
    /// A lookup for all of the regions by their assigned <see cref="RegionRoute"/>.
    /// </summary>
    public static ReadOnlyDictionary<PlatformRoute, Platform> RouteLookup { get; } = new(All.ToDictionary(x => x.Route, _ => _));

    /// <summary>
    /// A lookup for all of the regions by their assigned <see cref="RegionRoute"/>.
    /// </summary>
    public static ReadOnlyDictionary<PlatformCode, Platform> CodeLookup { get; } = new(All.ToDictionary(x => x.Code, _ => _));

    private Platform(PlatformRoute platformId, PlatformCode platformCode, string name)
    {
        Route = platformId;
        Code = platformCode;
        Name = name;
    }

    /// <summary>
    /// The unique route for the platform.
    /// </summary>
    public PlatformRoute Route { get; }

    /// <summary>
    /// The server platform code (differs from the id, which is server-specific.
    /// </summary>
    public PlatformCode Code { get; }

    /// <summary>
    /// The display for the platform.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Attempts to get the platform by route.
    /// </summary>
    /// <param name="platformRoute">The unique route for the platform.</param>
    /// <returns>The platform associated with the specified <see cref="PlatformRoute"/>.</returns>
    /// <exception cref="ArgumentException">Throws if the <see cref="PlatformRoute"/> specified was invalid.</exception>
    public static Platform FromRoute(PlatformRoute platformRoute) => RouteLookup.TryGetValue(platformRoute, out var platform) ? platform
        : throw new ArgumentException("Invalid platform route specified.", nameof(platformRoute));

    /// <summary>
    /// Attempts to get the platform by code.
    /// </summary>
    /// <param name="platformCode">The unique code for the platform.</param>
    /// <returns>The platform associated with the specified code.</returns>
    /// <exception cref="ArgumentException">Throws if the code specified was invalid.</exception>
    public static Platform FromCode(PlatformCode platformCode) => CodeLookup.TryGetValue(platformCode, out var platform) ? platform
        : throw new ArgumentException("Invalid platform code specified.", nameof(platformCode));
}
