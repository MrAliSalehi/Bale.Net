using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IAttachments
{
    /// <summary>
    /// send a photo to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="media">the photo to send</param>
    /// <param name="caption"> optional-caption</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the photo)</returns>
    ValueTask<Message> SendPhotoAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0);
    /// <summary>
    /// send a audio content to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="media">the audio to send</param>
    /// <param name="caption">optional-caption</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the audio)</returns>
    ValueTask<Message> SendAudioAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0);
    /// <summary>
    /// send a document to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="media">the document to send</param>
    /// <param name="caption">optional-caption</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the document)</returns>
    ValueTask<Message> SendDocumentAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0);
    /// <summary>
    /// send a video to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="media">the video to send</param>
    /// <param name="caption">optional-caption</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the video)</returns>
    ValueTask<Message> SendVideoAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0);
    /// <summary>
    /// send a voice content to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="media">the voice to send</param>
    /// <param name="caption">optional-caption</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the voice)</returns>
    ValueTask<Message> SendVoiceAsync(long chatId, Media media, string? caption = null, long replayToMessageId = 0);
    /// <summary>
    /// send a location to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="latitude">the target latitude</param>
    /// <param name="longitude">the target longitude</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the location)</returns>
    ValueTask<Message> SendLocationAsync(long chatId, double latitude, double longitude, long replayToMessageId = 0);
    /// <summary>
    /// share a contact in a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="phoneNumber"> phone number of the contact</param>
    /// <param name="firstName"> first name of the contact</param>
    /// <param name="lastName"> last name of the contact</param>
    /// <param name="replayToMessageId">optional-message id to replay</param>
    /// <returns>the sent message (including the contact)</returns>
    ValueTask<Message> SendContactAsync(long chatId, string phoneNumber, string firstName, string? lastName = "", long replayToMessageId = 0);
    /// <summary>
    /// get a file that was already uploaded to the server by id
    /// </summary>
    /// <param name="fileId">file id</param>
    /// <returns>the file stored in the server</returns>
    ValueTask<Types.File> GetFileAsync(string fileId);
}