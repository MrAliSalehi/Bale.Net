using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class ChatsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    public ChatsTest()
    {
        _client = new(Helpers.GetTestToken());
    }
    [Test]
    public async Task GetChat_ShouldGetMe()
    {
        var chat = await _client.Chats.GetChatAsync(MyChatId);
        
        Assert.Multiple(() =>
        {
            Assert.That(chat.Id, Is.EqualTo(MyChatId));
            Assert.That(chat, Is.Not.Null);
        });
        Assert.That(chat.Type,Is.EqualTo(ChatType.Private));
    }
}