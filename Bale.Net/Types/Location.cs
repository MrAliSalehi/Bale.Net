using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Location
{
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
}