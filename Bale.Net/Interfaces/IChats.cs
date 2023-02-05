using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IChats
{
    ValueTask<Chat> GetChatAsync(long chatId);
    ValueTask<ChatMember[]> GetChatAdministratorsAsync(long chatId);
    ValueTask<long> GetChatMembersCountAsync(long chatId);
}