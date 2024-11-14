using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IMessages
{
    /// <summary>
    /// send a message to a specific chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="message">the text message</param>
    /// <param name="replyMarkup">optional- replay markup keyboard</param>
    /// <param name="replayToMessageId">optional- message id to replay</param>
    /// <returns>the sent message</returns>
    ValueTask<Message> SendMessageAsync(ChatId chatId, string message, ReplyMarkup? replyMarkup = null, long replayToMessageId = 0);
    /// <summary>
    /// edit a text message or remove the replay markup keyboard from the message
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="messageId">id of the message to edit</param>
    /// <param name="message">new text message</param>
    /// <param name="replyMarkup">new replay markup keyboard (pass null to remove the keyboard)</param>
    /// <returns>the new edited message</returns>
    ValueTask<EditMessage> EditMessageTextAsync(ChatId chatId, long messageId, string message, ReplyMarkup? replyMarkup = null);
    /// <summary>
    /// delete a message from a chat
    /// </summary>
    /// <param name="chatId">id of the chat</param>
    /// <param name="messageId">message id</param>
    /// <returns>returns true if the message was deleted</returns>
    ValueTask<bool> DeleteMessageAsync(ChatId chatId, long messageId);
}