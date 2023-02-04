using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        //var response = await _client.Messages.SendMessageAsync();
    }
}