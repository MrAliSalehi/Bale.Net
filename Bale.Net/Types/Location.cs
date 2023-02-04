using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Location
{
    [JsonPropertyName("longitude")]
    public int Longitude { get; set; }

    [JsonPropertyName("latitude")]
    public int Latitude { get; set; }
}