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
    public async ValueTask<Message> SendMessageAsync(long chatId, string message, ReplyMarkup? replyMarkup = null, long replayToMessageId = 0)
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

        return await _client.PostAsync<SendMessageRequest, Message>(Endpoint.SendMessage, body);
    }
    public async ValueTask<EditMessage> EditMessageTextAsync(long chatId, long messageId, string message, ReplyMarkup? replyMarkup = null)
    {
        var body = new EditMessageRequest
        {
            ChatId = chatId,
            MessageId = messageId,
            Message = message
        };
        if (replyMarkup is not null)
            body.ReplyMarkup = replyMarkup;

        return await _client.PostAsync<EditMessageRequest, EditMessage>(Endpoint.EditMessage, body);
    }
    public async ValueTask<bool> DeleteMessageAsync(long chatId, long messageId)
    {
        var body = new DeleteMessageRequest
        {
            ChatId = chatId, MessageId = messageId
        };
        return await _client.PostAsync<DeleteMessageRequest, bool>(Endpoint.DeleteMessage, body);
    }
}