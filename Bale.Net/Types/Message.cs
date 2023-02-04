using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Message
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }

    [JsonPropertyName("from")]
    public User? From { get; set; }

    [JsonPropertyName("date")]
    public int Date { get; set; }

    [JsonPropertyName("chat")]
    public Chat? Chat { get; set; }

    [JsonPropertyName("forward_from")]
    public User? ForwardFrom { get; set; }

    [JsonPropertyName("forward_from_chat")]
    public Chat? ForwardFromChat { get; set; }

    [JsonPropertyName("forward_from_message_id")]
    public int ForwardFromMessageId { get; set; }

    [JsonPropertyName("forward_date")]
    public int ForwardDate { get; set; }

    [JsonPropertyName("reply_to_message")]
    public Message? ReplyToMessage { get; set; }

    [JsonPropertyName("edit_date")]
    public int EditDate { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("entities")]
    public Entities[]? Entities { get; set; }

    [JsonPropertyName("caption_entities")]
    public CaptionEntities[]? CaptionEntities { get; set; }

    [JsonPropertyName("audio")]
    public Audio? Audio { get; set; }

    [JsonPropertyName("document")]
    public Document? Document { get; set; }

    [JsonPropertyName("photo")]
    public Photo[]? Photo { get; set; }

    [JsonPropertyName("video")]
    public Video? Video { get; set; }

    [JsonPropertyName("voice")]
    public Voice? Voice { get; set; }

    [JsonPropertyName("caption")]
    public string? Caption { get; set; }

    [JsonPropertyName("contact")]
    public Contact? Contact { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("new_chat_members")]
    public Chat[]? NewChatMembers { get; set; }

    [JsonPropertyName("left_chat_member")]
    public Chat? LeftChatMember { get; set; }

    [JsonPropertyName("new_chat_title")]
    public string? NewChatTitle { get; set; }

    [JsonPropertyName("new_chat_photo")]
    public Photo[]? NewChatPhoto { get; set; }

    [JsonPropertyName("delete_chat_photo")]
    public bool DeleteChatPhoto { get; set; }

    [JsonPropertyName("group_chat_created")]
    public bool GroupChatCreated { get; set; }

    [JsonPropertyName("supergroup_chat_created")]
    public bool SupergroupChatCreated { get; set; }

    [JsonPropertyName("channel_chat_created")]
    public bool ChannelChatCreated { get; set; }

    [JsonPropertyName("pinned_message")]
    public Message? PinnedMessage { get; set; }

    [JsonPropertyName("invoice")]
    public Invoice? Invoice { get; set; }

    [JsonPropertyName("successful_payment")]
    public SuccessfulPayment? SuccessfulPayment { get; set; }
}