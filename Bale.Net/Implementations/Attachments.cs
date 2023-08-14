using System.Text;
using System.Text.Json;
using Bale.Net.Interfaces;
using Bale.Net.Types;
using Bale.Net.Types.Internal;
using File = Bale.Net.Types.File;

namespace Bale.Net.Implementations;

public class Attachments : IAttachments
{
    private readonly BaleClient _client;
    public Attachments(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Message> SendPhotoAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0) =>
        await SendAttachmentAsync(Endpoint.SendPhoto, "photo", chatId, media, caption, replayToMessageId);
    public async ValueTask<Message> SendAudioAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0) =>
        await SendAttachmentAsync(Endpoint.SendAudio, "audio", chatId, media, caption, replayToMessageId);
    public async ValueTask<Message> SendDocumentAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0) =>
        await SendAttachmentAsync(Endpoint.SendDocument, "document", chatId, media, caption, replayToMessageId);
    public async ValueTask<Message> SendVideoAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0) =>
        await SendAttachmentAsync(Endpoint.SendVideo, "video", chatId, media, caption, replayToMessageId);
    public async ValueTask<Message> SendVoiceAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0) =>
        await SendAttachmentAsync(Endpoint.SendVoice, "voice", chatId, media, caption, replayToMessageId);
    public async ValueTask<File> GetFileAsync(string fileId) =>
        await _client.GetAsync<File>(Endpoint.GetFile, $"?file_id={fileId}");
    public async ValueTask<Message> SendLocationAsync(long chatId, double latitude, double longitude, long replayToMessageId = 0)
    {
        var url = _client.ApiEndpoint.GetUrl(Endpoint.SendLocation);
        url += $"?chat_id={chatId}&{nameof(latitude)}={latitude}&{nameof(longitude)}={longitude}";

        if (replayToMessageId is not 0)
            url += $"reply_to_message_id={replayToMessageId}";

        var response = await _client.HttpClient.PostAsync(url, null);
        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<Message>>(await response.Content.ReadAsStringAsync());

        if (deserialize is null)
            throw new Exception("api failed to return any data,perhaps there are some internal errors");
        if (!deserialize.Ok)
            throw new Exception($"Failed to send photo with error code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }
    public async ValueTask<Message> SendContactAsync(long chatId, string phoneNumber, string firstName, string? lastName = "", long replayToMessageId = 0)
    {
        var body = new SendContactRequest
        {
            ChatId = chatId, PhoneNumber = phoneNumber, FirstName = firstName
        };
        if (lastName is not null)
            body.LastName = lastName;
        if (replayToMessageId is not 0)
            body.ReplyToMessageId = replayToMessageId;

        return await _client.PostAsync<SendContactRequest, Message>(Endpoint.SendContact, body);
    }


    private async ValueTask<Message> SendAttachmentAsync(Endpoint endpoint, string contentName, long chatId, Media media, string? caption = null, long replayToMessageId = 0)
    {
        var url = _client.ApiEndpoint.GetUrl(endpoint);

        
        using var body = media.GetContent(contentName);
        body.Add(new StringContent(chatId.ToString()),"chat_id");
        if (caption is not null)
            body.Add(new StringContent(caption!),nameof(caption));
        
        var response = await _client.HttpClient.PostAsync(url,body );
        var content = JsonSerializer.Deserialize<BaseApiResponse<Message>>(await response.Content.ReadAsStringAsync());

        if (content is null)
            throw new Exception("api failed to return any data,perhaps there are some internal errors");

        if (!content.Ok)
            throw new Exception($"Failed to send the Attachment with error code:[{content.ErrorCode}],description:{content.Description}");

        return content.Result;
    }
}