using System.Text.Json.Serialization;

namespace Bale.Net.Types;
#nullable disable
public sealed class Message
{
    [JsonPropertyName("chat_id")] public int ChatId { get; set; }

    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("reply_markup")] public ReplyMarkup ReplyMarkup { get; set; }

    [JsonPropertyName("reply_to_message_id")]
    public int ReplyToMessageId { get; set; }
}