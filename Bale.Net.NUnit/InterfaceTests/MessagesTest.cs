using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class MessagesTest
{
    private readonly BaleClient _client;
    private const string Text = "hello";
    private const long MyId = 2047754943;
    private const long TestBotId = 102472526;
    private const long ValidMessageId = 1750352069;
    public MessagesTest()
    {
        _client = new(Helpers.GetTestToken());
    }
    [Test]
    public async Task SendMessage_Should_SendMessage()
    {
        var response = await _client.Messages.SendMessageAsync(MyId, Text);
        var now = DateTime.Now;

        Assert.Multiple( () =>
        {
            Assert.That(response.MessageId, Is.Not.Zero);
            Assert.That(response.Chat, Is.Not.Null);
            Assert.That(response.Text, Is.EqualTo(Text));
            Assert.That(response.Chat!.Id, Is.EqualTo(MyId));
            Assert.That(response.From, Is.Not.Null);
            Assert.That(response.From!.Id, Is.EqualTo(TestBotId));

            Assert.That(response.Date.Year, Is.EqualTo(now.Year));
            Assert.That(response.Date.Month, Is.EqualTo(now.Month));
            Assert.That(response.Date.Day, Is.EqualTo(now.Day));
            Assert.That(response.Date.Hour, Is.EqualTo(now.Hour));
            Assert.That(response.Date.Minute, Is.EqualTo(now.Minute));
        });
    }
    [Test]
    public async Task SendMessage_WithReplayToMessage_ShouldReplay()
    {
        var response = await _client.Messages.SendMessageAsync(MyId, Text, replayToMessageId: ValidMessageId);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.Text, Is.EqualTo(Text));
            Assert.That(response.From!.Id, Is.EqualTo(TestBotId));
            Assert.That(response.Chat!.Id, Is.EqualTo(MyId));
        });
    }
}