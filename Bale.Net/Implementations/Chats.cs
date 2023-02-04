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
    public async ValueTask<Chat> GetChatAsync(long chatId)
    {
        return await _client.GetAsync<Chat>(Endpoint.GetChat, $"?chat_id={chatId}");
    }
}