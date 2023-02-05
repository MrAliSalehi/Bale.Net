using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class AttachmentsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    private const long TestBotId = 102472526;


    private static readonly Uri ValidPhotoUrl = new("https://www.linkpicture.com/q/Screenshot_20230205_112447.png");
    private static readonly Uri ValidAudioUrl = new("https://cdn.pixabay.com/download/audio/2022/09/07/audio_460d86d83e.mp3?filename=beautiful-random-minor-arp-119378.mp3");
    public AttachmentsTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }

    [Test, TestCaseSource(nameof(PhotoMediaSource))]
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
    private static IEnumerable<Media> PhotoMediaSource()
    {
        var validPhotoPath = Path.Combine(Environment.CurrentDirectory, "Screenshot_20230205_112447.png");
        const string validPhotoFileId = "102472526:6574097720140177152:1:deceef14d610758990901060a34bb34a34a99430d2cf2c91dd4224f7e84b244f";

        yield return Media.FromDisk(validPhotoPath);
        Task.Delay(600); //to avoid rate limit
        yield return Media.FromUrl(ValidPhotoUrl);
        Task.Delay(600);
        yield return Media.FromId(validPhotoFileId);
    }
    private static IEnumerable<Media> AudioMediaSource()
    {
        var validAudioPath = Path.Combine(Environment.CurrentDirectory, "beautiful-random-minor-arp-119378.mp3");
        const string validAudioFileId = "";

        yield return Media.FromUrl(ValidAudioUrl);
        yield return Media.FromDisk(validAudioPath);
        yield return Media.FromId(validAudioFileId);
    }
}