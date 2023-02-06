using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Prices
{
    [JsonPropertyName("label")]public string? Label { get; set; }
    [JsonPropertyName("amount")]public int Amount { get; set; }
}