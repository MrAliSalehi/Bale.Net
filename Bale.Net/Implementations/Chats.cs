using Bale.Net.Interfaces;
using Bale.Net.Types;

namespace Bale.Net.Implementations;

public class Chats : IChats
{
    private readonly BaleClient _client;
    public Chats(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Chat> GetChatAsync(long chatId) =>
        await _client.GetAsync<Chat>(Endpoint.GetChat, $"?chat_id={chatId}");
    public async ValueTask<ChatMember[]> GetChatAdministratorsAsync(long chatId) =>
        await _client.GetAsync<ChatMember[]>(Endpoint.GetChatAdministrators, $"?chat_id={chatId}");
    public async ValueTask<long> GetChatMembersCountAsync(long chatId) =>
        await _client.GetAsync<long>(Endpoint.GetChatMembersCount, $"?chat_id={chatId}");
}