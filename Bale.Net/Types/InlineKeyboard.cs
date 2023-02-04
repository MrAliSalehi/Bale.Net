using System.Text.Json.Serialization;

namespace Bale.Net.Types;
#nullable disable
public sealed class InlineKeyboard
{
    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("callback_data")] public string CallbackData { get; set; }
}