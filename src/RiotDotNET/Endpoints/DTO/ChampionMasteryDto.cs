﻿namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a response dto for riot api champion mastery requests.
/// </summary>
public class ChampionMasteryDto
{
    /// <summary>
    /// Number of points needed to achieve next level. Zero if player reached maximum champion level for this champion.
    /// </summary>
    [JsonPropertyName("championPointsUntilNextLevel")]
    public long ChampionPointsUntilNextLevel { get; set; }

    /// <summary>
    /// Is chest granted for this champion or not in current season.
    /// </summary>
    [JsonPropertyName("chestGranted")]
    public bool ChestGranted { get; set; }

    /// <summary>
    /// Champion ID for this entry.
    /// </summary>
    [JsonPropertyName("championId")]
    public long ChampionId { get; set; }

    /// <summary>
    /// Last time this champion was played by this player - in Unix milliseconds time format.
    /// </summary>
    [JsonPropertyName("lastPlayTime")]
    public long LastPlayTime { get; set; }

    /// <summary>
    /// Champion level for specified player and champion combination.
    /// </summary>
    [JsonPropertyName("championLevel")]
    public int ChampionLevel { get; set; }

    /// <summary>
    /// Summoner ID for this entry. (Encrypted)
    /// </summary>
    [JsonPropertyName("summonerId")]
    public string SummonerId { get; set; } = default!;

    /// <summary>
    /// Total number of champion points for this player and champion combination - they are used to determine championLevel.
    /// </summary>
    [JsonPropertyName("championPoints")]
    public int ChampionPoints { get; set; }

    /// <summary>
    /// Number of points earned since current level has been achieved.
    /// </summary>
    [JsonPropertyName("championPointsSinceLastLevel")]
    public long ChampionPointsSinceLastLevel { get; set; }

    /// <summary>
    /// The token earned for this champion at the current championLevel. When the championLevel is advanced the tokensEarned resets to 0.
    /// </summary>
    [JsonPropertyName("tokensEarned")]
    public int TokensEarned { get; set; }
}
