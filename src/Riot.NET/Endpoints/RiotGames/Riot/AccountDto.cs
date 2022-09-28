namespace RiotNET.Endpoints.RiotGames.Riot;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a response dto for riot api account requests.
/// </summary>
public class AccountDto
{
    /// <summary>
    /// Globally unique player user id.
    /// </summary>
    [JsonPropertyName("puuid")]
    public string Puuid { get; set; } = default!;

    /// <summary>
    /// The player's game name.
    /// Note: This field may be excluded from the response if the account doesn't have a game name.
    /// </summary>
    [JsonPropertyName("gameName")]
    public string? Name { get; set; }

    /// <summary>
    /// The player's tag line.
    /// Note: This field may be excluded from the response if the account doesn't have a tagLine.
    /// </summary>
    [JsonPropertyName("tagLine")]
    public string? Tag { get; set; }
}
