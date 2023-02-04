using Bale.Net.Interfaces;
using Bale.Net.Types;

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
        if (replayToMessageId is not 0)
            body.ReplyToMessageId = replayToMessageId;
        
        var response = await _client.PostAsync<SendMessageRequest,Message>(Endpoint.SendMessage,body);
        
        
        return response;
    }
}