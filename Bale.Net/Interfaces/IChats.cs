using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IChats
{
    /// <summary>
    /// get a chat by id
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>the targeted chat</returns>
    ValueTask<Chat> GetChatAsync(ChatId chatId);
    /// <summary>
    /// get all the admins of a chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>administrators</returns>
    [Obsolete("it seems that this endpoint has been removed from the api")]
    ValueTask<ChatMember[]> GetChatAdministratorsAsync(ChatId chatId);
    /// <summary>
    /// count of the members of a chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>members count</returns>
    ValueTask<long> GetChatMembersCountAsync(ChatId chatId);
    ValueTask<bool> LeaveChatAsync(ChatId chatId);
    /// <summary>
    /// get a user from a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="userId">id of the user</param>
    /// <returns>the user</returns>
    [Obsolete("it seems that this endpoint has been removed from the api")]
    ValueTask<ChatMember> GetChatMemberAsync(ChatId chatId, long userId);
}