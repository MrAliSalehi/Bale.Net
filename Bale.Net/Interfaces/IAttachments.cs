using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IAttachments
{
    ValueTask<Message> SendPhotoAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
    ValueTask<Message> SendAudioAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
    ValueTask<Message> SendDocumentAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
    ValueTask<Message> SendVideoAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
    ValueTask<Message> SendVoiceAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
}