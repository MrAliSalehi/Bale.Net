using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public class EditMessage
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }

    [JsonPropertyName("chat")]
    public Chat? Chat { get; set; }

    private int _dateUtc;
    [JsonInclude]
    [JsonPropertyName("date")]
    public int DateUtc
    {
        get => _dateUtc;
        set
        {
            _dateUtc = value;
            Date = DateTimeOffset.FromUnixTimeSeconds(value).LocalDateTime;
        }
    }
    public DateTime Date { get; set; }


    private int _editDate;
    [JsonInclude]
    [JsonPropertyName("edit_date")]
    public int EditDateUtc
    {
        get => _editDate;
        set
        {
            _editDate = value;
            EditDate = DateTimeOffset.FromUnixTimeSeconds(value).LocalDateTime;
        }
    }
    public DateTime EditDate { get; set; }
}