using System.Text.Json.Serialization;
using Bale.Net.Updates;

namespace Bale.Net.Types;

public sealed class ShippingQuery : IUpdateType
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("from")]
    public User? From { get; set; }

    [JsonPropertyName("invoice_payload")]
    public string? InvoicePayload { get; set; }

    [JsonPropertyName("shipping_address")]
    public ShippingAddress? ShippingAddress { get; set; }
}