using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class OrderInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("shipping_address")]
    public ShippingAddress? ShippingAddress { get; set; }
}