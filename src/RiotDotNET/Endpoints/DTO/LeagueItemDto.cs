namespace RiotDotNET.Endpoints.DTO;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class LeagueItemDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("leagueId")]
    public string LeagueId { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("entries")]
    public List<LeagueItemDto> Entries { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tier")]
    public string Tier { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("queue")]
    public string Queue { get; set; } = default!;
}
