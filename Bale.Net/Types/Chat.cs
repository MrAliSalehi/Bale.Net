﻿using System.Text.Json.Serialization;

namespace Bale.Net.Types;

public sealed class Chat
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ChatType Type { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("all_members_are_administrators")]
    public bool AllMembersAreAdministrators { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("invite_link")]
    public string? InviteLink { get; set; }

    [JsonPropertyName("pinned_message")]
    public Message? PinnedMessage { get; set; }

    [JsonPropertyName("sticker_set_name")]
    public string? StickerSetName { get; set; }

    [JsonPropertyName("can_set_sticker_set")]
    public bool CanSetStickerSet { get; set; }
    public static implicit operator long(Chat chat) => chat.Id;
}