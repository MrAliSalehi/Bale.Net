using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class ChatMember
{
    [JsonPropertyName("user")]
    public User? User { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    //api doesnt return these....
    /*[JsonPropertyName("until_date")]
    public int UntilDate { get; set; }

    [JsonPropertyName("can_be_edited")]
    public bool CanBeEdited { get; set; }

    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; set; }

    [JsonPropertyName("can_post_messages")]
    public bool CanPostMessages { get; set; }

    [JsonPropertyName("can_edit_messages")]
    public bool CanEditMessages { get; set; }

    [JsonPropertyName("can_delete_messages")]
    public bool CanDeleteMessages { get; set; }

    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; set; }

    [JsonPropertyName("can_restrict_members")]
    public bool CanRestrictMembers { get; set; }

    [JsonPropertyName("can_pin_messages")]
    public bool CanPinMessages { get; set; }

    [JsonPropertyName("can_promote_members")]
    public bool CanPromoteMembers { get; set; }

    [JsonPropertyName("can_send_messages")]
    public bool CanSendMessages { get; set; }

    [JsonPropertyName("can_send_media_messages")]
    public bool CanSendMediaMessages { get; set; }

    [JsonPropertyName("can_send_other_messages")]
    public bool CanSendOtherMessages { get; set; }

    [JsonPropertyName("can_add_web_page_previews")]
    public bool CanAddWebPagePreviews { get; set; }*/
}