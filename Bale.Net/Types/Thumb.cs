using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Thumb
{
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("file_size")]
    public int FileSize { get; set; }
}