namespace RiotDotNET.Endpoints.DTO;

using RiotDotNET.Enums;
using System.Text.Json.Serialization;

public class PlayerDto
{
    /// <summary>
    /// The player's summoner id.
    /// </summary>
    [JsonPropertyName("summonerId")]
    public string SummonerId { get; set; } = default!;

    /// <summary>
    /// The player's clash team id.
    /// </summary>
    [JsonPropertyName("teamId")]
    public string TeamId { get; set; } = default!;

    /// <summary>
    /// (Legal values: UNSELECTED, FILL, TOP, JUNGLE, MIDDLE, BOTTOM, UTILITY)
    /// </summary>
    [JsonPropertyName("position")]
    public ClashPosition Position { get; set; } = default!;

    /// <summary>
    /// (Legal values: CAPTAIN, MEMBER)
    /// </summary>
    [JsonPropertyName("role")]
    public ClashRole Role { get; set; } = default!;
}
