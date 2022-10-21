namespace RiotDotNET.Endpoints.RiotGames.LoL;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a response dto for riot api summoner requests.
/// </summary>
public class SummonerDto
{
    /// <summary>
    /// Encrypted summoner ID. Max length 63 characters.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Encrypted account ID. Max length 56 characters.
    /// </summary>
    [JsonPropertyName("accountId")]
    public string AccountId { get; set; } = default!;

    /// <summary>
    /// Encrypted PUUID. Exact length of 78 characters.
    /// </summary>
    [JsonPropertyName("puuid")]
    public string Puuid { get; set; } = default!;

    /// <summary>
    /// Summoner name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// ID of the summoner icon associated with the summoner.
    /// </summary>
    [JsonPropertyName("profileIconId")]
    public int ProfileIconId { get; set; }

    /// <summary>
    /// Date summoner was last modified specified as epoch milliseconds.
    /// The following events will update this timestamp: summoner name change, summoner level change, or profile icon change.
    /// </summary>
    [JsonPropertyName("revisionDate")]
    public long RevisionDateMilliseconds { get; set; }

    /// <summary>
    /// The following events will update this timestamp: summoner name change, summoner level change, or profile icon change.
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset RevisionDate
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(RevisionDateMilliseconds);
        set => RevisionDateMilliseconds = value.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// ID of the summoner icon associated with the summoner.
    /// </summary>
    [JsonPropertyName("summonerLevel")]
    public long Level { get; set; }
}
