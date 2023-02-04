using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class MessagesTest
{
    private readonly BaleClient _client;
    public MessagesTest()
    {
        _client = new(Helpers.GetTestToken());
    }
    [Test]
    public async Task SendMessage_Should_SendMessage()
    {
        var keyboard = new ReplyMarkup
        {
            InlineKeyboard = new []{new []{new InlineKeyboard(){Text = "test1",CallbackData = "testd"}}},
            Keyboard = new []{new []{new Keyboard{Text = "keyboard"}}}
        };
        var response = await _client.Messages.SendMessageAsync(2047754943,"hello",keyboard);
    }
}