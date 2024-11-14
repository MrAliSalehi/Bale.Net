using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bale.Net.Types;

// taken from :
// https://github.com/TelegramBots/Telegram.Bot/blob/master/src/Telegram.Bot/Serialization/ChatIdConverter.cs
// https://github.com/TelegramBots/Telegram.Bot/blob/master/src/Telegram.Bot/Types/ChatId.cs

/// <summary>
/// Represents a ChatId
/// </summary>
[JsonConverter(typeof(ChatIdConverter))]
public class ChatId : IEquatable<ChatId>
{
    /// <summary>
    /// Unique identifier for the chat
    /// </summary>
    public long? Identifier { get; }

    /// <summary>
    /// Username of the supergroup or channel (in the format @channelusername)
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat
    /// </summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public ChatId(long identifier) => Identifier = identifier;

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)
    /// </summary>
    /// <param name="username">Unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)</param>
    /// <exception cref="ArgumentException">
    /// Thrown when string value isn`t number and doesn't start with @
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when string value is <c>null</c></exception>
    public ChatId(string username)
    {
        if (username is null) { throw new ArgumentNullException(nameof(username)); }
        if (username.Length > 1 && username[0] == '@')
        {
            Username = username;
        }
        else if (long.TryParse(
            s: username,
            style: NumberStyles.Integer,
            provider: CultureInfo.InvariantCulture,
            result: out var identifier))
        {
            Identifier = identifier;
        }
        else
        {
            throw new ArgumentException("Username value should be Identifier or Username that starts with @", nameof(username));
        }
    }

    public override bool Equals(object? obj) =>
        obj switch
        {
            ChatId chatId => this == chatId,
            _ => false,
        };

    public bool Equals(ChatId? other) => this == other;

    public override int GetHashCode() => StringComparer.InvariantCulture.GetHashCode(ToString());

    public override string ToString() => (Username ?? Identifier?.ToString(CultureInfo.InvariantCulture))!;

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat
    /// </summary>
    /// <param name="identifier">Unique identifier for the chat</param>
    public static implicit operator ChatId(long identifier) => new(identifier);

    /// <summary>
    /// Create a <see cref="ChatId"/> using unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)
    /// </summary>
    /// <param name="username">Unique identifier for the chat or username of
    /// the supergroup or channel (in the format @channelusername)</param>
    /// <exception cref="ArgumentException">
    /// Thrown when string value isn`t number and doesn't start with @
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when string value is <c>null</c></exception>
    public static implicit operator ChatId(string username) => new(username);

    /// <summary>
    /// Convert a <see cref="Chat"/> object to a <see cref="ChatId"/>
    /// </summary>
    /// <param name="chat"></param>
    [return: NotNullIfNotNull(nameof(chat))]
    public static implicit operator ChatId?(Chat? chat) => chat is null ? null : new(chat.Id);

    /// <summary>
    /// Compares two ChatId objects
    /// </summary>
    public static bool operator ==(ChatId? obj1, ChatId? obj2)
    {
        if (obj1 is null || obj2 is null) { return false; }

        if (obj1.Identifier is not null && obj2.Identifier is not null)
        {
            return obj1.Identifier == obj2.Identifier;
        }

        if (obj1.Username is not null && obj2.Username is not null)
        {
            return string.Equals(obj1.Username, obj2.Username, StringComparison.Ordinal);
        }

        return false;
    }

    /// <summary>
    /// Compares two ChatId objects
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    public static bool operator !=(ChatId obj1, ChatId obj2) => !(obj1 == obj2);
}
internal class ChatIdConverter : JsonConverter<ChatId?>
{
    public override ChatId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonElement.TryParseValue(ref reader, out var element))
            return null;

        return new(element.Value.ToString());
    }

    public override void Write(Utf8JsonWriter writer, ChatId? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case { Username: {} username }:
                writer.WriteStringValue(username);
                break;
            case { Identifier: {} identifier }:
                writer.WriteNumberValue(identifier);
                break;
            case null:
                writer.WriteNullValue();
                break;
            default:
                throw new JsonException("Chat ID value is incorrect");
        }
    }
}