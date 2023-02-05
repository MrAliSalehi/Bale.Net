using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IAttachments
{
    ValueTask<Message> SendPhotoAsync(long chatId,Media media,string? caption = null,long replayToMessageId = 0);
}