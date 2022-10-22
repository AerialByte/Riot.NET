namespace RiotDotNET.Endpoints.DTO;
using System.Text.Json.Serialization;

public class LocalizedInfo
{
    /// <summary>
    /// The localized challenge description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    /// <summary>
    /// The localized challenge name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// The localized challenge short description.
    /// </summary>
    [JsonPropertyName("shortDescription")]
    public string ShortDescription { get; set; } = default!;
}
