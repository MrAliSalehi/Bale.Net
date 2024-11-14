using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Bale.Net.Types.Internal;

public sealed class DeleteMessageRequest
{
    [JsonPropertyName("chat_id")]
    public required ChatId ChatId { get; set; }
    
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
}