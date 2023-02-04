using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class CaptionEntities
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("user")]
    public User? User { get; set; }
}