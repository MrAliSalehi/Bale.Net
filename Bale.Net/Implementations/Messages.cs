using Bale.Net.Interfaces;
using Bale.Net.Types;
using Bale.Net.Types.Internal;

namespace Bale.Net.Implementations;

public class Messages : IMessages
{
    private readonly BaleClient _client;
    public Messages(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Message> SendMessageAsync(ChatId chatId, string message, ReplyMarkup? replyMarkup = null, long replayToMessageId = 0)
    {
        var body = new SendMessageRequest
        {
            ChatId = chatId,
            Text = message
        };
        if (replyMarkup is not null)
            body.ReplyMarkup = replyMarkup;

        if (replayToMessageId != 0)
            body.ReplyToMessageId = replayToMessageId;

        return await _client.TryPostAsync<SendMessageRequest, Message>(Endpoint.SendMessage, body);
    }
    public async ValueTask<EditMessage> EditMessageTextAsync(ChatId chatId, long messageId, string message, ReplyMarkup? replyMarkup = null)
    {
        var body = new EditMessageRequest
        {
            ChatId = chatId,
            MessageId = messageId,
            Message = message
        };
        if (replyMarkup is not null)
            body.ReplyMarkup = replyMarkup;

        return await _client.TryPostAsync<EditMessageRequest, EditMessage>(Endpoint.EditMessage, body);
    }
    public ValueTask<Message> ForwardMessageAsync(ChatId chatId, ChatId fromChatId, long msgId)
    {
        return FwdOrCpyMessageAsync(chatId,fromChatId,msgId, Endpoint.ForwardMessage);
    }

    public ValueTask<Message> CopyMessageAsync(ChatId chatId, ChatId fromChatId, long msgId)
    {
        return FwdOrCpyMessageAsync(chatId,fromChatId,msgId, Endpoint.CopyMessage);
    }

    public async ValueTask<bool> DeleteMessageAsync(ChatId chatId, long messageId)
    {
        var body = new DeleteMessageRequest
        {
            ChatId = chatId, MessageId = messageId
        };
        return await _client.TryPostAsync<DeleteMessageRequest, bool>(Endpoint.DeleteMessage, body);
    }
    private async ValueTask<Message> FwdOrCpyMessageAsync(ChatId chatId, ChatId fromChatId, long msgId, Endpoint e)
    {
        var body = new ForwardOrCopyMessageRequest
        {
            ChatId = chatId, MessageId = msgId, FromChatId = fromChatId
        };
        return await _client.TryPostAsync<ForwardOrCopyMessageRequest, Message>(e, body);
    }
}