using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("language_code")]
    public string LanguageCode { get; set; }

    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }
}