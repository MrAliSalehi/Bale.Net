using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class AttachmentsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    private const long TestBotId = 102472526;
    private const string ValidPhotoPath = @"C:\Users\Ali\Pictures\Screenshots\Screenshot_20221103_091537.png";
    public AttachmentsTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }
    [Test]
    public async Task SendPhoto_ShouldSendPhoto()
    {
        var message = await _client.Attachments.SendPhotoAsync(MyChatId, Media.FromDisk(ValidPhotoPath), "test photo");
        Assert.Multiple(() =>
        {
            Assert.That(message.Caption, Is.Not.Null.Or.Empty);
            Assert.That(message.Photo, Has.Length.Positive.Within(1));
        });
        Assert.That(message.Photo![0].FileId, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Photo[0].Height, Is.Not.Zero.Or.Empty);
            Assert.That(message.Photo[0].Width, Is.Not.Zero.Or.Empty);
            Assert.That(message.Photo[0].FileSize, Is.Not.Zero.Or.Empty);
            Assert.That(message.Chat, Is.Not.Null.Or.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
        });
        Assert.That(message.From!.Id, Is.EqualTo(TestBotId));
        
        await Task.Delay(300);
        await _client.Messages.DeleteMessageAsync(message.Chat!.Id, message.MessageId);
    }
}