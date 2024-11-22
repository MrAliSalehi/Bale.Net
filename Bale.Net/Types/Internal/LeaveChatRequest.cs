using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class LeaveChatRequest
{
    [JsonPropertyName("chat_id")]
    public required ChatId ChatId { get; set; }
}