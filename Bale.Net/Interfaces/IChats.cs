using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IChats
{
    /// <summary>
    /// get a chat by id
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>the targeted chat</returns>
    ValueTask<Chat> GetChatAsync(long chatId);
    /// <summary>
    /// get all the admins of a chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>administrators</returns>
    ValueTask<ChatMember[]> GetChatAdministratorsAsync(long chatId);
    /// <summary>
    /// count of the members of a chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <returns>members count</returns>
    ValueTask<long> GetChatMembersCountAsync(long chatId);
    /// <summary>
    /// get a user from a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="userId">id of the user</param>
    /// <returns>the user</returns>
    ValueTask<ChatMember> GetChatMemberAsync(long chatId,long userId);
}