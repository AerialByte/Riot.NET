using RiotNET.Enums;

namespace RiotNET.Services.Riot;

/// <summary>
/// Defines the configuration config for riot api instances.
/// </summary>
public class RiotApiOptions
{
    /// <summary>
    /// The api key to use when executing riot api requests.
    /// </summary>
    public string ApiKey { get; set; } = default!;

    /// <summary>
    /// The rate limits for each riot api region and platform endpoint. na1, americas, etc have different rate limits.
    /// </summary>
    public Dictionary<TimeSpan, int> RateLimits { get; set; } = new();

    /// <summary>
    /// Optionally, specify the rate limits specific to each region. Default to values specified in <see cref="RateLimits"/>.
    /// </summary>
    public Dictionary<RegionRoute, Dictionary<TimeSpan, int>> RegionRateLimits { get; set; } = new();

    /// <summary>
    /// Optionally, specify the rate limits specific to each platform. Default to values specified in <see cref="RateLimits"/>.
    /// </summary>
    public Dictionary<PlatformRoute, Dictionary<TimeSpan, int>> PlatformRateLimits { get; set; } = new();
}
