using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class MessagesTest
{
    private readonly BaleClient _client = new(Helpers.GetTestToken());
    private const string Text = "hello";
    private const long MyId = 2047754943;
    private const long TestBotId = 102472526;
    private const long ValidMessageId = 1750352069;
    private const long ValidBotMessageIdToEdit = -1863136851;

    private static readonly ReplyMarkup ValidInlineKeyboard = new()
    {
        InlineKeyboard = new[]
        {
            new[]
            {
                new InlineKeyboard { Text = "first inline", CallbackData = "first callback", SwitchInlineQuery = "", SwitchInlineQueryCurrentChat = "", Pay = false },
                new InlineKeyboard { Text = "some link", Url = "https://www.google.com" }
            }
        }
    };

    [Test]
    public async Task SendMessage_Should_SendMessage()
    {
        var response = await _client.Messages.SendMessageAsync(MyId, Text);
        var now = DateTime.Now;
        Console.WriteLine($"message id: {response.MessageId}");
        Assert.Multiple(() =>
        {
            Assert.That(response.Chat, Is.Not.Null);
            Assert.That(response.From, Is.Not.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(response.MessageId, Is.Not.Zero);
            Assert.That(response.Text, Is.EqualTo(Text));
            Assert.That(response.Chat!.Id, Is.EqualTo(MyId));
            Assert.That(response.From!.Id, Is.EqualTo(TestBotId));

            Assert.That(response.Date.Year, Is.EqualTo(now.Year));
            Assert.That(response.Date.Month, Is.EqualTo(now.Month));
            Assert.That(response.Date.Day, Is.EqualTo(now.Day));
            Assert.That(response.Date.Hour, Is.EqualTo(now.Hour));
            Assert.That(response.Date.Minute, Is.EqualTo(now.Minute));
        });
    }

    [Test]
    public async Task SendMessage_WithReplayToMessageAndKeyboard_ShouldSendMessage()
    {
        var response = await _client.Messages.SendMessageAsync(MyId, Text, ValidInlineKeyboard, ValidMessageId);

        await Task.Delay(1000);

        var buttonKeyboard = new ReplyMarkup { Keyboard = new[] { new[] { new Keyboard { Text = "button1", RequestContact = false, RequestLocation = false }, new Keyboard { Text = "send contact", RequestContact = true } } }, };
        var response2 = await _client.Messages.SendMessageAsync(MyId, Text, buttonKeyboard, ValidMessageId);


        Assert.Multiple(() =>
        {
            Assert.That(response.Text, Is.EqualTo(Text));
            Assert.That(response.From!.Id, Is.EqualTo(TestBotId));
            Assert.That(response.Chat!.Id, Is.EqualTo(MyId));
        });

        Assert.Multiple(() =>
        {
            Assert.That(response2.Text, Is.EqualTo(Text));
            Assert.That(response2.From!.Id, Is.EqualTo(TestBotId));
            Assert.That(response2.Chat!.Id, Is.EqualTo(MyId));
        });
    }
    [Test]
    public async Task EditMessage_ShouldEditMessage()
    {
        var randomNumber = Random.Shared.Next(int.MaxValue - 1);
        var response = await _client.Messages.EditMessageTextAsync(MyId, ValidBotMessageIdToEdit, $"new edit_{randomNumber}", ValidInlineKeyboard);
        var now = DateTime.Now;
        Assert.Multiple(() =>
        {
            Assert.That(response.Chat, Is.Not.Null);
            Assert.That(response, Is.Not.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(response.Chat!.Id, Is.EqualTo(MyId));
            Assert.That(response.DateUtc, Is.EqualTo(response.EditDateUtc));

            Assert.That(response.Date.Year, Is.EqualTo(now.Year));
            Assert.That(response.Date.Month, Is.EqualTo(now.Month));
            Assert.That(response.Date.Day, Is.EqualTo(now.Day));
            Assert.That(response.Date.Hour, Is.EqualTo(now.Hour));
            Assert.That(response.Date.Minute, Is.EqualTo(now.Minute));
        });
    }

    [Test]
    public async Task DeleteMessage_ShouldDeleteMessage()
    {
        const string msg = "message to delete";
        var message = await _client.Messages.SendMessageAsync(MyId, msg);

        Assert.Multiple(() =>
        {
            Assert.That(message.Text, Is.EqualTo(msg));
            Assert.That(message.MessageId, Is.Not.Zero);
        });
        await Task.Delay(500); //avoid rate limit
        var response = await _client.Messages.DeleteMessageAsync(MyId, message.MessageId);

        Assert.That(response, Is.True);
    }

    [Test]
    public async Task SendInvalidMessage_ShouldRetry()
    {
        const int invalidChatId = -1111111111;
        _client.Delay = TimeSpan.FromSeconds(1);
        _client.MaxRetryAttempts = 2;
        _client.OnRetry = static arg =>
        {
            Console.WriteLine(arg.AttemptNumber);
            return ValueTask.CompletedTask;
        };
        var attempts = _client.RetryAttempts;
        try
        {
            await _client.Messages.SendMessageAsync(invalidChatId, "some msg");
        }
        catch
        {
            //ignore
        }

        Assert.That(_client.RetryAttempts, Is.Not.EqualTo(attempts));
    }
}