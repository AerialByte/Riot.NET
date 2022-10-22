namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

public class LeagueEntryDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("leagueId")]
    public string LeagueId { get; set; } = default!;

    /// <summary>
    /// Player's summonerId (Encrypted)
    /// </summary>
    [JsonPropertyName("summonerId")]
    public string SummonerId { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("summonerName")]
    public string SummonerName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("queueType")]
    public string QueueType { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tier")]
    public string Tier { get; set; } = default!;

    /// <summary>
    /// The player's division within a tier.
    /// </summary>
    [JsonPropertyName("rank")]
    public string Rank { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("leaguePoints")]
    public int LeaguePoints { get; set; }

    /// <summary>
    /// Winning team on Summoners Rift. First placement in Teamfight Tactics.
    /// </summary>
    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    /// <summary>
    /// Losing team on Summoners Rift. Second through eighth placement in Teamfight Tactics.
    /// </summary>
    [JsonPropertyName("losses")]
    public int Losses { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("hotStreak")]
    public bool HotStreak { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("veteran")]
    public bool Veteran { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("freshBlood")]
    public bool FreshBlood { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("inactive")]
    public bool Inactive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("miniSeries")]
    public MiniSeriesDto MiniSeries { get; set; } = default!;
}
