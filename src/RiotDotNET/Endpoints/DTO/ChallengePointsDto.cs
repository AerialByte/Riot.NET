namespace RiotDotNET.Endpoints.DTO;
using RiotDotNET.Enums;
using System.Text.Json.Serialization;

public class ChallengePointsDto
{
    [JsonPropertyName("level")]
    public LeagueRankTier Level { get; set; }

    [JsonPropertyName("current")]
    public long Current { get; set; }

    [JsonPropertyName("max")]
    public long Max { get; set; }

    [JsonPropertyName("percentile")]
    public double Percentile { get; set; }
}
