using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class EditMessageRequest
{
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
    [JsonPropertyName("text")]
    public string? Message { get; set; }
    
    [JsonPropertyName("replay_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }
}