using System.Text.Json.Serialization;
using Bale.Net.Updates;

namespace Bale.Net.Types;

public sealed class CallbackQuery : IUpdateType
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("from")]
    public User? From { get; set; }

    [JsonPropertyName("message")]
    public Message? Message { get; set; }

    [JsonPropertyName("inline_message_id")]
    public string? InlineMessageId { get; set; }

    [JsonPropertyName("chat_instance")]
    public string? ChatInstance { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("game_short_name")]
    public string? GameShortName { get; set; }
}