namespace RiotDotNET.Endpoints.DTO;

using System.Text.Json.Serialization;

public class PlayerClientPreferencesDto
{
    [JsonPropertyName("bannerAccent")]
    public string BannerAccent { get; set; } = default!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    [JsonPropertyName("ChallengeIds")]
    public List<long> ChallengeIds { get; set; } = default!;
}
