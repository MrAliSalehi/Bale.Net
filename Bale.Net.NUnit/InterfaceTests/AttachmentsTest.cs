using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class AttachmentsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    private const long TestBotId = 102472526;
    private const string ValidPhotoPath = @"C:\Users\Ali\Pictures\Screenshots\Screenshot_20221103_091537.png";
    private const string ValidPhotoFileId = "102472526:6574097720140177152:1:deceef14d610758990901060a34bb34a34a99430d2cf2c91dd4224f7e84b244f";
    private static readonly Uri ValidPhotoUrl = new("https://www.linkpicture.com/q/Screenshot_20230205_112447.png");
    public AttachmentsTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }

    [Test, TestCaseSource(nameof(MediaSource))]
    public async Task SendPhoto_WithAllMediaTypes_ShouldSendPhoto(Media media)
    {
        var message = await _client.Attachments.SendPhotoAsync(MyChatId, media, "test photo");
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
    private static IEnumerable<Media> MediaSource()
    {
        yield return Media.FromDisk(ValidPhotoPath);
        Task.Delay(600); //to avoid rate limit
        yield return Media.FromUrl(ValidPhotoUrl);
        Task.Delay(600);
        yield return Media.FromId(ValidPhotoFileId);
    }
}