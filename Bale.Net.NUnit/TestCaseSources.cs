using Bale.Net.Types;

namespace Bale.Net.NUnit;

internal static class TestCaseSources
{
    private static readonly Uri ValidPhotoUrl = new("https://www.linkpicture.com/q/Screenshot_20230205_112447.png");
    private static readonly Uri ValidAudioUrl = new("https://filebin.net/8aphk5vlrnnfg2rd/beautiful-random-minor-arp-119378.mp3");
    private static readonly Uri ValidDocUrl = new("https://filebin.net/8aphk5vlrnnfg2rd/Bale.Net.NUnit.pdb");
    private static readonly Uri ValidVidUrl = new("https://filebin.net/8aphk5vlrnnfg2rd/testVid.mp4");
    private static readonly Uri ValidVoiceUrl = new("https://filebin.net/8aphk5vlrnnfg2rd/voice.ogg");
    internal static IEnumerable<Media> AudioMediaSource()
    {
        var validAudioPath = Path.Combine(Environment.CurrentDirectory, "beautiful-random-minor-arp-119378.mp3");
        const string validAudioFileId = "102472526:7431267519825714946:1:26be31450335e139";

        yield return Media.FromUrl(ValidAudioUrl);
        Task.Delay(600);
        yield return Media.FromDisk(validAudioPath);
        Task.Delay(600);
        yield return Media.FromId(validAudioFileId);
    }
    internal static IEnumerable<Media> DocumentMediaSource()
    {
        var validDocPath = Path.Combine(Environment.CurrentDirectory, "Bale.Net.pdb");
        const string validDocFileId = "102472526:-8500608615372218621:1:26be31450335e139";

        yield return Media.FromDisk(validDocPath);
        Task.Delay(600);
        yield return Media.FromUrl(ValidDocUrl);
        Task.Delay(600);
        yield return Media.FromId(validDocFileId);
    }
    internal static IEnumerable<Media> VideoMediaSource()
    {
        var validVidPath = Path.Combine(Environment.CurrentDirectory, "testVid.mp4");
        const string validVidFileId = "102472526:9111538882509545218:1:26be31450335e139";

        yield return Media.FromDisk(validVidPath);
        Task.Delay(600);
        yield return Media.FromUrl(ValidVidUrl);
        Task.Delay(600);
        yield return Media.FromId(validVidFileId);
    }
    internal static IEnumerable<Media> VoiceMediaSource()
    {
        var validVoicePath = Path.Combine(Environment.CurrentDirectory, "voice.ogg");
        const string validVoiceFileId = "102472526:-7181420884580819199:1:26be31450335e139";

        yield return Media.FromDisk(validVoicePath);
        Task.Delay(600);
        yield return Media.FromUrl(ValidVoiceUrl);
        Task.Delay(600);
        yield return Media.FromId(validVoiceFileId);
    }
    internal static IEnumerable<Media> PhotoMediaSource()
    {
        var validPhotoPath = Path.Combine(Environment.CurrentDirectory, "Screenshot_20230205_112447.png");
        const string validPhotoFileId = "102472526:6574097720140177152:1:deceef14d610758990901060a34bb34a34a99430d2cf2c91dd4224f7e84b244f";

        yield return Media.FromDisk(validPhotoPath);
        Task.Delay(600);
        yield return Media.FromUrl(ValidPhotoUrl);
        Task.Delay(600);
        yield return Media.FromId(validPhotoFileId);
    }
}