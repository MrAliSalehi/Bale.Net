using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public enum ChatType
{
    [JsonPropertyName("private")] Private,
    [JsonPropertyName("group")] Group,
    [JsonPropertyName("supergroup")] Supergroup,
    [JsonPropertyName("channel")] Channel
}