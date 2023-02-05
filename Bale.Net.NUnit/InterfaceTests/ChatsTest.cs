﻿using Bale.Net.Types;

namespace Bale.Net.NUnit.InterfaceTests;

public class ChatsTest
{
    private readonly BaleClient _client;
    private const long MyChatId = 2047754943;
    private const long MyGroupChatId = 5734091716;
    public ChatsTest()
    {
        _client = new(Helpers.GetTestToken());
    }
    [Test]
    public async Task GetChat_ShouldGetMe()
    {
        var chat = await _client.Chats.GetChatAsync(MyChatId);

        Assert.Multiple(() =>
        {
            Assert.That(chat.Id, Is.EqualTo(MyChatId));
            Assert.That(chat, Is.Not.Null);
        });
        Assert.That(chat.Type, Is.EqualTo(ChatType.Private));
    }
    [Test]
    public async Task GetChatAdministrator_ShouldGetAdmins()
    {
        var chatMembers = await _client.Chats.GetChatAdministratorsAsync(MyGroupChatId);

        Assert.That(chatMembers, Has.Length.Positive.Within(1));
        Assert.Multiple(() =>
        {
            Assert.That(chatMembers.Count(p => p.Status == "creator"), Is.EqualTo(1));
            Assert.That(chatMembers.Count(p => p.User.Id == MyChatId), Is.EqualTo(1));
        });
    }
}