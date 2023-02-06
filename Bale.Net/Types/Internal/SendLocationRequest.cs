using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

internal sealed class SendLocationRequest
{
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
    [JsonPropertyName("reply_to_message_id")]
    public long ReplayToMessageId { get; set; }
}