namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

public class LeagueListDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("freshBlood")]
    public bool FreshBlood { get; set; }

    /// <summary>
    /// Winning team on Summoners Rift.
    /// </summary>
    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("summonerName")]
    public string SummonerName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("miniSeries")]
    public MiniSeriesDto MiniSeries { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("inactive")]
    public bool Inactive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("veteran")]
    public bool Veteran { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("hotStreak")]
    public bool HotStreak { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("rank")]
    public string Rank { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("leaguePoints")]
    public int LeaguePoints { get; set; }

    /// <summary>
    /// Losing team on Summoners Rift.
    /// </summary>
    [JsonPropertyName("losses")]
    public int Losses { get; set; }

    /// <summary>
    /// Player's encrypted summonerId.
    /// </summary>
    [JsonPropertyName("summonerId")]
    public string SummonerId { get; set; } = default!;
}
