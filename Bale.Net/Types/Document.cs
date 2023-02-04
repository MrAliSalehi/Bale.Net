using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Document
{
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    [JsonPropertyName("thumb")]
    public Thumb? Thumb { get; set; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }

    [JsonPropertyName("file_size")]
    public int FileSize { get; set; }
}