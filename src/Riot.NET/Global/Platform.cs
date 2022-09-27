namespace RiotNET.Global;

using RiotNET.Enums;
using RiotNET.Interfaces;
using System.Collections.ObjectModel;

/// <summary>
/// Defines the various riot api server platforms.
/// </summary>
public sealed class Platform : IHost
{
    /// <summary>
    /// Brazil (BR): br1.api.riotgames.com
    /// </summary>
    public static Platform Brazil { get; } = new(PlatformId.BR1, PlatformCode.BR, "Brazil");

    /// <summary>
    /// Europe Nordic & East (EUNE): eun1.api.riotgames.com
    /// </summary>
    public static Platform EuropeNordicEast { get; } = new(PlatformId.EUN1, PlatformCode.EUNE, "Europe Nordic & East");

    /// <summary>
    /// Europe West (EUW): euw1.api.riotgames.com
    /// </summary>
    public static Platform EuropeWest { get; } = new(PlatformId.EUW1, PlatformCode.EUW, "Europe West");

    /// <summary>
    /// Japan (JP): jp1.api.riotgames.com
    /// </summary>
    public static Platform Japan { get; } = new(PlatformId.JP1, PlatformCode.JP, "Japan");

    /// <summary>
    /// Republic of Korea (KR): kr.api.riotgames.com
    /// </summary>
    public static Platform Korea { get; } = new(PlatformId.KR, PlatformCode.KR, "Korea");

    /// <summary>
    /// Latin America North (LAN): la1.api.riotgames.com
    /// </summary>
    public static Platform LatinAmericaNorth { get; } = new(PlatformId.LA1, PlatformCode.LAN, "Latin America North");

    /// <summary>
    /// Latin America South (LAS): la2.api.riotgames.com
    /// </summary>
    public static Platform LatinAmericaSouth { get; } = new(PlatformId.LA2, PlatformCode.LAS, "Latin America South");

    /// <summary>
    /// North America (NA): na1.api.riotgames.com
    /// </summary>
    public static Platform NorthAmerica { get; } = new(PlatformId.NA1, PlatformCode.NA, "North America");

    /// <summary>
    /// Oceania (OCE): oc1.api.riotgames.com
    /// </summary>
    public static Platform Oceania { get; } = new(PlatformId.OC1, PlatformCode.OCE, "Oceania");

    /// <summary>
    /// Turkey (TR): tr1.api.riotgames.com
    /// </summary>
    public static Platform Turkey { get; } = new(PlatformId.TR1, PlatformCode.TR, "Turkey");

    /// <summary>
    /// Russia (RU): ru.api.riotgames.com
    /// </summary>
    public static Platform Russia { get; } = new(PlatformId.RU, PlatformCode.RU, "Russia");

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
    /// A lookup for all of the regions by their assigned <see cref="RegionId"/>.
    /// </summary>
    public static ReadOnlyDictionary<PlatformId, Platform> ById { get; } = new(All.ToDictionary(x => x.Id, _ => _));

    /// <summary>
    /// A lookup for all of the regions by their assigned <see cref="RegionId"/>.
    /// </summary>
    public static ReadOnlyDictionary<PlatformCode, Platform> ByCode { get; } = new(All.ToDictionary(x => x.Code, _ => _));

    /// <summary>
    /// A lookup for all of the regions by their defined <see cref="Name"/>.
    /// </summary>
    public static ReadOnlyDictionary<string, Platform> ByName { get; } = new(All.ToDictionary(x => x.Name, _ => _));

    private Platform(PlatformId platformId, PlatformCode platformCode, string name, string? hostPrefix = null, string riotApiHost = HostDefaults.RiotGamesApi)
    {
        Id = platformId;
        Code = platformCode;
        Name = name;
        Host = $"{hostPrefix ?? Id.ToString().ToLower()}.{riotApiHost}";
    }

    /// <summary>
    /// The unique identifier for the platform.
    /// </summary>
    public PlatformId Id { get; }

    /// <summary>
    /// The server platform code (differs from the id, which is server-specific.
    /// </summary>
    public PlatformCode Code { get; }

    /// <summary>
    /// The display for the platform.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The full host for connections to the riot api with this platform.
    /// </summary>
    public string Host { get; }
}
