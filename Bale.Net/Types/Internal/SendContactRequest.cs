using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class SendContactRequest
{
    [JsonPropertyName("chat_id")]
    public long ChatId { get; set; }
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
    [JsonPropertyName("reply_to_message_id")]
    public long ReplyToMessageId { get; set; }
}

