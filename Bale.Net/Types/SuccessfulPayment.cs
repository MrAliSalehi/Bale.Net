using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class SuccessfulPayment
{
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("total_amount")]
    public int TotalAmount { get; set; }

    [JsonPropertyName("invoice_payload")]
    public string? InvoicePayload { get; set; }

    [JsonPropertyName("shipping_option_id")]
    public string? ShippingOptionId { get; set; }

    [JsonPropertyName("order_info")]
    public OrderInfo? OrderInfo { get; set; }

    [JsonPropertyName("telegram_payment_charge_id")]
    public string? TelegramPaymentChargeId { get; set; }

    [JsonPropertyName("provider_payment_charge_id")]
    public string? ProviderPaymentChargeId { get; set; }
}