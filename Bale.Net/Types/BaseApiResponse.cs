using System.Text.Json.Serialization;

namespace Bale.Net.Types;
#nullable disable
public class BaseApiResponse<T>
{
    
    [JsonPropertyName("result")] 
    public T Result { get; set; }
    
    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("ok")]
    public bool Ok { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}