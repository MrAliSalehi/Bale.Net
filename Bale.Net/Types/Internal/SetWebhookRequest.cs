using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class SetWebhookRequest
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}