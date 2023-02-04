using System.Text.Json.Serialization;

namespace Bale.Net.Types.Internal;

public class GetUpdatesRequest
{
    [JsonPropertyName("offset")]
    public long Offset { get; set; }
    [JsonPropertyName("limit")]
    public long Limit { get; set; }
}