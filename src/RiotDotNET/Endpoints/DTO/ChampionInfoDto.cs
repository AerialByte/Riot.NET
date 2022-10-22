namespace RiotDotNET.Endpoints.DTO;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a response dto for riot api champion info requests.
/// </summary>
public class ChampionInfoDto
{
    /// <summary>
    /// Level for players to be able to select from <see cref="FreeChampionIdsForNewPlayers"/> list.
    /// </summary>
    [JsonPropertyName("maxNewPlayerLevel")]
    public int MaxNewPlayerLevel { get; set; }

    /// <summary>
    /// The champion id roatation that players under level in <see cref="MaxNewPlayerLevel"/> can play.
    /// </summary>
    [JsonPropertyName("freeChampionIdsForNewPlayers")]
    public List<int> FreeChampionIdsForNewPlayers { get; set; } = default!;

    /// <summary>
    /// The current free champion rotations.
    /// </summary>
    [JsonPropertyName("freeChampionIds")]
    public List<int> FreeChampionIds { get; set; } = default!;

}
