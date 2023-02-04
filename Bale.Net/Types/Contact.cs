using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Contact
{
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
}