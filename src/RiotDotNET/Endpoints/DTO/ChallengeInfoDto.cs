namespace RiotDotNET.Endpoints.DTO;

using RiotDotNET.Enums;
using System.Text.Json.Serialization;

public class ChallengeInfoDto
{
    [JsonPropertyName("challengeId")]
    public long ChallengeId { get; set; } = default!;

    [JsonPropertyName("percentile")]
    public double Percentile { get; set; } = default!;

    [JsonPropertyName("level")]
    public LeagueRankTier Level { get; set; } = default!;

    [JsonPropertyName("value")]
    public long Value { get; set; } = default!;

    [JsonPropertyName("achievedTime")]
    public long AchievedTimeMilliseconds { get; set; } = default!;

    [JsonIgnore]
    public DateTimeOffset AchievedTime
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(AchievedTimeMilliseconds);
        set => AchievedTimeMilliseconds = value.ToUnixTimeMilliseconds();
    }
}
