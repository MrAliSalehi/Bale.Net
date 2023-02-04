using System.Text.Json.Serialization;

namespace Bale.Net.Types;
#nullable disable
public sealed class ReplyMarkup
{
    [JsonPropertyName("keyboard")]
    public Keyboard[][] Keyboard { get; set; }

    [JsonPropertyName("inline_keyboard")]
    public InlineKeyboard[][] InlineKeyboard { get; set; }

    [JsonPropertyName("resize_keyboard")]
    public bool ResizeKeyboard { get; set; }

    [JsonPropertyName("one_time_keyboard")]
    public bool OneTimeKeyboard { get; set; }

    [JsonPropertyName("selective")]
    public bool Selective { get; set; }
}