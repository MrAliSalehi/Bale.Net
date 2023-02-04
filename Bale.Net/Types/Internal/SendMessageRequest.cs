using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

internal sealed class SendMessageRequest
{
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("reply_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }

    [JsonPropertyName("reply_to_message_id")]
    public long ReplyToMessageId { get; set; }
}