using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class InlineKeyboard
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("switch_inline_query")]
    public string? SwitchInlineQuery { get; set; }

    [JsonPropertyName("switch_inline_query_current_chat")]
    public string? SwitchInlineQueryCurrentChat { get; set; }

    [JsonPropertyName("pay")]
    public bool Pay { get; set; }
}