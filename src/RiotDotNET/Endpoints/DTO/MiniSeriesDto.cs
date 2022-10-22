namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

public class MiniSeriesDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("losses")]
    public int Losses { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("progress")]
    public string Progress { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("target")]
    public int Target { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("wins")]
    public int Wins { get; set; }
}
