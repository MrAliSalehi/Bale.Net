using System.Text.Json.Serialization;
using Bale.Net.Implementations;

namespace Bale.Net.Types;

public sealed class Update
{
    [JsonPropertyName("update_id")]
    public long UpdateId { get; set; }

    [JsonPropertyName("message")]
    public Message? Message { get; set; }

    [JsonPropertyName("edited_message")]
    public Message? EditedMessage { get; set; }

    [JsonPropertyName("channel_post")]
    public Message? ChannelPost { get; set; }

    [JsonPropertyName("edited_channel_post")]
    public Message? EditedChannelPost { get; set; }

    [JsonPropertyName("callback_query")]
    public CallbackQuery? CallbackQuery { get; set; }

    [JsonPropertyName("shipping_query")]
    public ShippingQuery? ShippingQuery { get; set; }

    [JsonPropertyName("pre_checkout_query")]
    public PreCheckoutQuery? PreCheckoutQuery { get; set; }
}