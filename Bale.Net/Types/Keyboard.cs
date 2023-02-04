using System.Text.Json.Serialization;

namespace Bale.Net.Types;
#nullable disable
public sealed class Keyboard
{
    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("request_contact")] public bool RequestContact { get; set; }
    [JsonPropertyName("request_location")] public bool RequestLocation { get; set; }
}