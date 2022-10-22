namespace RiotDotNET.Endpoints.DTO;

using RiotDotNET.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ChallengeConfigInfoDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("localizedNames")]
    public Dictionary<string, LocalizedInfo> localizedNames { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("state")]
    public ChallengeState State { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tracking")]
    public ChallengeTracking Tracking { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("startTimestamp")]
    public long StartTimestamp { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("endTimestamp")]
    public long EndTimestamp { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("leaderboard")]
    public bool Leaderboard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("thresholds")]
    public Dictionary<LeagueRankTier, double> Thresholds { get; set; } = default!;
}
