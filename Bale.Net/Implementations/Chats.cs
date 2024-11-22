using Bale.Net.Interfaces;
using Bale.Net.Types;
using Bale.Net.Types.Internal;

namespace Bale.Net.Implementations;

public class Chats : IChats
{
    private readonly BaleClient _client;
    public Chats(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Chat> GetChatAsync(ChatId chatId) =>
        await _client.TryGetAsync<Chat>(Endpoint.GetChat, $"?chat_id={chatId}");
    public async ValueTask<ChatMember[]> GetChatAdministratorsAsync(ChatId chatId) =>
        await _client.TryGetAsync<ChatMember[]>(Endpoint.GetChatAdministrators, $"?chat_id={chatId}");
    public async ValueTask<long> GetChatMembersCountAsync(ChatId chatId) =>
        await _client.TryGetAsync<long>(Endpoint.GetChatMembersCount, $"?chat_id={chatId}");
    public async ValueTask<ChatMember> GetChatMemberAsync(ChatId chatId, long userId) =>
        await _client.TryGetAsync<ChatMember>(Endpoint.GetChatMember, $"?chat_id={chatId}&user_id={userId}");
    public async ValueTask<bool> LeaveChatAsync(ChatId chatId) => 
        await _client.TryPostAsync<LeaveChatRequest, bool>(Endpoint.LeaveChat, new LeaveChatRequest { ChatId = chatId });
}