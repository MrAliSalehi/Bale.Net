using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IMessages
{
    ValueTask<Message> SendMessageAsync(long chatId, string message, ReplyMarkup? replyMarkup = null, long replayToMessageId = 0);
    ValueTask<EditMessage> EditMessageTextAsync(long chatId,long messageId,string message,ReplyMarkup? replyMarkup = null);
    ValueTask<bool> DeleteMessageAsync(long chatId,long messageId);
}