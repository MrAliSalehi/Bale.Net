using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class AttachmentsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    private const long TestBotId = 102472526;
    private static IEnumerable<Media> _photoMediaSource = TestCaseSources.PhotoMediaSource();
    private static IEnumerable<Media> _audioMediaSource = TestCaseSources.AudioMediaSource();
    private static IEnumerable<Media> _documentMediaSource = TestCaseSources.DocumentMediaSource();
    private static IEnumerable<Media> _videoMediaSource = TestCaseSources.VideoMediaSource();
    private static IEnumerable<Media> _voiceMediaSource = TestCaseSources.VoiceMediaSource();
    public AttachmentsTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }


    [Test, TestCaseSource(nameof(_photoMediaSource))]
    public async Task SendPhoto_ShouldSendPhoto(Media media)
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

    [Test, TestCaseSource(nameof(_audioMediaSource))]
    public async Task SendAudio_ShouldSendAudio(Media media)
    {
        var message = await _client.Attachments.SendAudioAsync(MyChatId, media, "test Audio");

        Assert.That(message.Audio, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Audio!.FileId, Is.Not.Null.Or.Empty);
            Assert.That(message.Audio.FileSize, Is.Not.Zero.Or.Empty);
            Assert.That(message.Caption, Is.Not.Null.Or.Empty);
            Assert.That(message.Audio.MimeType, Is.Not.Null.Or.Empty);
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
        });
    }

    [Test, TestCaseSource(nameof(_documentMediaSource))]
    public async Task SendDoc_ShouldSendDoc(Media media)
    {
        var message = await _client.Attachments.SendDocumentAsync(MyChatId, media, "test doc");

        Assert.That(message.Document, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Document!.FileId, Is.Not.Null.Or.Empty);
            Assert.That(message.Document.FileSize, Is.Not.Zero.Or.Empty);
            Assert.That(message.Document.FileName, Is.Not.Null.Or.Empty);
            Assert.That(message.Document.MimeType, Is.Not.Null.Or.Empty);
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
            Assert.That(message.Caption, Is.Not.Null.Or.Empty);
        });
    }

    [Test, TestCaseSource(nameof(_videoMediaSource))]
    public async Task SendVideo_ShouldSendVideo(Media media)
    {
        var message = await _client.Attachments.SendVideoAsync(MyChatId, media, "test vid");

        Assert.That(message.Video, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Video!.FileId, Is.Not.Null.Or.Empty);
            Assert.That(message.Video.FileSize, Is.Not.Zero.Or.Empty);
            Assert.That(message.Video.Height, Is.Not.Zero.Or.Empty);
            Assert.That(message.Video.MimeType, Is.Not.Null.Or.Empty);
            Assert.That(message.Video.Width, Is.Not.Zero.Or.Empty);
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
            Assert.That(message.Caption, Is.Not.Null.Or.Empty);
        });
    }

    [Test, TestCaseSource(nameof(_voiceMediaSource))]
    public async Task SendVoice_ShouldSendVoice(Media media)
    {
        var message = await _client.Attachments.SendVoiceAsync(MyChatId, media, "test voice");

        Assert.That(message.Voice, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Voice!.FileId, Is.Not.Null.Or.Empty);
            Assert.That(message.Voice.FileSize, Is.Not.Zero.Or.Empty);
            Assert.That(message.Voice.MimeType, Is.Not.Null.Or.Empty);
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
            Assert.That(message.Caption, Is.Not.Null.Or.Empty);
        });
    }

    [Test]
    public async Task SendLocation_ShouldSendLocation()
    {
        var message = await _client.Attachments.SendLocationAsync(MyChatId, 35.69253, 51.41734);

        Assert.That(message.Location, Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(message.Location, Is.Not.Null.Or.Empty);
            Assert.That(message.Location!.Latitude, Is.Not.Zero.Or.Empty);
            Assert.That(message.Location.Longitude, Is.Not.Zero.Or.Empty);
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From, Is.Not.Null);
        });
    }

    [Test]
    public async Task SendContact_ShouldSendContact()
    {
        var message = await _client.Attachments.SendContactAsync(MyChatId, "9330001112", "fiName", "LName");
        
        Assert.That(message.Contact,Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(message.Contact!.FirstName, Is.Not.Null.Or.Empty);
            Assert.That(message.Contact.LastName, Is.Not.Null.Or.Empty);
            Assert.That(message.Contact.PhoneNumber, Is.Not.Null.Or.Empty);
            Assert.That(message.Chat, Is.Not.Null.Or.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(message.Chat!.Id, Is.EqualTo(MyChatId));
            Assert.That(message.From!.Id, Is.EqualTo(TestBotId));
        });
    }
    [Test]
    public async Task GetFile_ShouldGetFile()
    {
        var file = await _client.Attachments.GetFileAsync(TestCaseSources.ValidAudioFileId);
        
        Assert.That(file,Is.Not.Null.Or.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(file.FileId, Is.EqualTo(TestCaseSources.ValidAudioFileId));
            Assert.That(file.FileSize, Is.Not.Zero);
            Assert.That(file.FilePath, Is.Not.Null.Or.Empty);
        });
    }
}