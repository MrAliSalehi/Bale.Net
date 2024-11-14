using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class InvoiceRequest
{
    [JsonPropertyName("chat_id")]
    public required ChatId ChatId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    [JsonPropertyName("provider_token")]
    public string? ProviderToken { get; set; }

    [JsonPropertyName("start_parameter")]
    public string? StartParameter { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("prices")]
    public Prices[]? Prices { get; set; }

    [JsonPropertyName("provider_data")]
    public string? ProviderData { get; set; }

    [JsonPropertyName("photo_url")]
    public string? PhotoUrl { get; set; }

    [JsonPropertyName("photo_size")]
    public int PhotoSize { get; set; }

    [JsonPropertyName("photo_width")]
    public int PhotoWidth { get; set; }

    [JsonPropertyName("photo_height")]
    public int PhotoHeight { get; set; }

    [JsonPropertyName("need_name")]
    public bool NeedName { get; set; }

    [JsonPropertyName("need_phone_number")]
    public bool NeedPhoneNumber { get; set; }

    [JsonPropertyName("need_email")]
    public bool NeedEmail { get; set; }

    [JsonPropertyName("need_shipping_address")]
    public bool NeedShippingAddress { get; set; }

    [JsonPropertyName("is_flexible")]
    public bool IsFlexible { get; set; }

    [JsonPropertyName("disable_notification")]
    public bool DisableNotification { get; set; }

    [JsonPropertyName("reply_to_message_id")]
    public int ReplyToMessageId { get; set; }

    [JsonPropertyName("reply_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }
}