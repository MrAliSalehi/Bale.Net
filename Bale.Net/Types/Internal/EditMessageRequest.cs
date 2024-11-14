using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Bale.Net.Types.Internal;

public sealed class EditMessageRequest
{
    [JsonPropertyName("chat_id")]
    public required ChatId ChatId { get; set; }
    
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
    
    [JsonPropertyName("text")]
    public string? Message { get; set; }
    
    [JsonPropertyName("replay_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }
}