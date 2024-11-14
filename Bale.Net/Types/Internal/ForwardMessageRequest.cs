using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class ForwardMessageRequest
{
    [JsonPropertyName("chat_id")]
    public required ChatId ChatId { get; set; }

    [JsonPropertyName("from_chat_id")]
    public required ChatId FromChatId { get; set; }

    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
}