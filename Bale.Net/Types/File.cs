using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class File
{
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }
    
    [JsonPropertyName("file_size")]
    public int FileSize { get; set; }
    [JsonPropertyName("file_path")]
    public string? FilePath { get; set; }

}