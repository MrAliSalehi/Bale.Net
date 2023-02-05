using System.Text.Json;
using Bale.Net.Interfaces;
using Bale.Net.Types;

namespace Bale.Net.Implementations;

public class Attachments : IAttachments
{
    private readonly BaleClient _client;
    public Attachments(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Message> SendPhotoAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0)
    {
        var url = _client.ApiEndpoint.GetUrl(Endpoint.SendPhoto);
        url += $"?chat_id={chatId}";
        if (caption is not null)
            url += $"&caption={caption}";

        if (replayToMessageId is not 0)
            url+=$"&reply_to_message_id={replayToMessageId}";
        
        var response = await _client.HttpClient.PostAsync(url, media.GetContent("photo"));
        var content = JsonSerializer.Deserialize<BaseApiResponse<Message>>(await response.Content.ReadAsStringAsync());

        if (content is null)
            throw new Exception("api failed to return any data,perhaps there are some internal errors");

        if (!content.Ok)
            throw new Exception($"Failed to send photo with error code:[{content.ErrorCode}],description:{content.Description}");

        return content.Result;
    }
}