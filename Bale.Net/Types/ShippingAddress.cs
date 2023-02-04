using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class ShippingAddress
{
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("stat")]
    public string? Stat { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("street_line1")]
    public string? StreetLine1 { get; set; }

    [JsonPropertyName("street_line2")]
    public string? StreetLine2 { get; set; }

    [JsonPropertyName("post_code")]
    public string? PostCode { get; set; }
}