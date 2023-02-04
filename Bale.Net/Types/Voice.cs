using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Voice
{
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }

    [JsonPropertyName("file_size")]
    public int FileSize { get; set; }
}