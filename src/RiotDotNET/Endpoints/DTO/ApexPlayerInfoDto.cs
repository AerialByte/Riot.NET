namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

public class ApexPlayerInfoDto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("puuid")]
    public string Puuid { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("position")]
    public int Position { get; set; }
}
