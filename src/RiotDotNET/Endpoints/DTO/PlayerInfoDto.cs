namespace RiotDotNET.Endpoints.DTO;
using RiotDotNET.Enums;
using System.Text.Json.Serialization;

public class PlayerInfoDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("challenges")]
    public List<ChallengeInfoDto> Challenges { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("preferences")]
    public PlayerClientPreferencesDto Preferences { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("totalPoints")]
    public ChallengePointsDto TotalPoints { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("categoryPoints")]
    public Dictionary<ChallengeCategory, ChallengePointsDto> CategoryPoints { get; set; } = default!;

}
