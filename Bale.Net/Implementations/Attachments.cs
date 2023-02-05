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
    public async ValueTask<Message> SendPhotoAsync(long chatId, Media media, string? caption = null)
    {
        var url = _client.ApiEndpoint.GetUrl(Endpoint.SendPhoto);
        url += $"?chat_id={chatId}";
        if (caption is not null)
            url += $"&caption={caption}";

        var response = await _client.HttpClient.PostAsync(url, media.Content);
        var content = JsonSerializer.Deserialize<BaseApiResponse<Message>>(await response.Content.ReadAsStringAsync());

        if (content is null)
            throw new Exception("api failed to return any data,perhaps there are some internal errors");

        if (!content.Ok)
            throw new Exception($"Failed to send photo with error code:[{content.ErrorCode}],description:{content.Description}");

        return content.Result;
    }
}