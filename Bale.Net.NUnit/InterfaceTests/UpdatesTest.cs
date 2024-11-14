using Bale.Net.Interfaces;
using Bale.Net.Types;
using Bale.Net.Updates;
using Bogus;

namespace Bale.Net.NUnit.InterfaceTests;

public class UpdatesTest
{
    private readonly BaleClient _client = new(Helpers.GetTestToken());
    private const long MyId = 2047754943;
    [Test]
    public async Task SetWebHook_Should_Set()
    {
        var response = await _client.Updates.SetWebHookAsync("https://127.0.0.1:6969");

        Assert.That(response, Is.True);
    }
    [Test]
    public async Task DeleteWebhook_Should_Delete()
    {
        var response = await _client.Updates.DeleteWebHookAsync();

        Assert.That(response, Is.True);
    }

    [Test]
    public async Task Update_Should_GetUpdates()
    {
        var updates = await _client.Updates.GetUpdatesAsync(-1, 0);

        var update = updates.First();

        Assert.That(updates, Is.Not.Empty.Or.Null);
        Assert.Multiple(() =>
        {
            Assert.That(updates, Has.Length.Positive.Within(5));
            Assert.That(update.Message, Is.Not.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(update.Message!.Chat, Is.Not.Null);
            Assert.That(update.Message!.From, Is.Not.Null);

            Assert.That(update.Message.From!.Id, Is.EqualTo(MyId));
            Assert.That(update.Message.Chat!.Id, Is.EqualTo(MyId));
        });
    }
    internal static int UpdateCount;
    internal static int ErrCount;

    [Test]
    public async Task UpdateReceiver_ShouldReceiveUpdates()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdates()
        };

        client.Updates.ReceiveUpdates<UpdateHandler>();
        await Task.Delay(TimeSpan.FromSeconds(3));
        Assert.That(UpdateCount, Is.EqualTo(TestUpdates.IncomingUpdateCount));
        client.Updates.StopReceiving();
    }
    [Test]
    public async Task GenericUpdateReceiver_ShouldReceiveUpdates()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdates(),
        };
        client.Updates.ReceiveUpdates<GenericUpdateHandler, Message>(TimeSpan.FromSeconds(1));
        await Task.Delay(TimeSpan.FromSeconds(7));
        Assert.That(UpdateCount, Is.EqualTo(TestUpdates.IncomingUpdateCount));
        client.Updates.StopReceiving();
        UpdateCount = 0;
    }

    [Test]
    public async Task UpdateReceiver_ShouldSendErrors()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdates()
        };
        client.Updates.ReceiveUpdates<UpdateHandler>();
        await Task.Delay(TimeSpan.FromSeconds(3));
        Assert.Multiple(() =>
        {
            Assert.That(UpdateCount, Is.EqualTo(TestUpdates.IncomingUpdateCount));
            Assert.That(ErrCount, Is.EqualTo(5));
        });
        client.Updates.StopReceiving();
    }
    [Test]
    public async Task GenericUpdateReceiver_ShouldSendErrors()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdates()
        };
        client.Updates.ReceiveUpdates<GenericUpdateHandler, Message>(TimeSpan.FromSeconds(1));
        await Task.Delay(TimeSpan.FromSeconds(5));
        Assert.Multiple(() =>
        {
            Assert.That(UpdateCount, Is.EqualTo(TestUpdates.IncomingUpdateCount));
            Assert.That(ErrCount, Is.EqualTo(5));
        });
        client.Updates.StopReceiving();
    }

    [Test]
    public async Task UpdateReceiver_ShouldHandleGetUpdate_Errors()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdatesFails()
        };
        client.Updates.ReceiveUpdates<UpdateHandler>();
        await Task.Delay(TimeSpan.FromSeconds(3));
        Assert.Multiple(() => { Assert.That(ErrCount, Is.EqualTo(TestUpdatesFails.Count - 1)); });
        client.Updates.StopReceiving();
        ErrCount = 0;
    }
    [Test]
    public async Task GenericUpdateReceiver_ShouldHandleGetUpdate_Errors()
    {
        var client = new BaleClient("")
        {
            Updates = new TestUpdatesFails()
        };
        client.Updates.ReceiveUpdates<GenericUpdateHandler, Message>();
        await Task.Delay(TimeSpan.FromSeconds(3));
        Assert.Multiple(() => { Assert.That(ErrCount, Is.EqualTo(TestUpdatesFails.Count - 1)); });
        client.Updates.StopReceiving();
        ErrCount = 0;
    }
}

file class UpdateHandler : IUpdateHandler
{
    public ValueTask ReceiveUpdateAsync(Update update, CancellationToken ct = default)
    {
        UpdatesTest.UpdateCount += 1;
        if (UpdatesTest.UpdateCount <= 5)
            throw new Exception("test exception");

        return ValueTask.CompletedTask;
    }
    public ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default)
    {
        UpdatesTest.ErrCount += 1;
        return ValueTask.CompletedTask;
    }
}

file class GenericUpdateHandler : IUpdateHandler<Message>
{
    public ValueTask ReceiveUpdateAsync(Message update, CancellationToken ct = default)
    {
        UpdatesTest.UpdateCount += 1;
        if (UpdatesTest.UpdateCount <= 5)
            throw new Exception("test exception");

        return ValueTask.CompletedTask;
    }
    public ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default)
    {
        UpdatesTest.ErrCount += 1;
        return ValueTask.CompletedTask;
    }
}

file class TestUpdates : IUpdates
{
    public const int IncomingUpdateCount = 10;
    private readonly Faker<Update> _faker = new();
    private readonly Faker<Message> _msgFaker = new();
    private int _count;
    public ValueTask<Update[]> GetUpdatesAsync(long offset, long limit) => new(_count++ < 2
                                                                                   ? _faker
                                                                                       .CustomInstantiator(_ => new Update
                                                                                       {
                                                                                           Message = _msgFaker.Generate(1).First(),
                                                                                       })
                                                                                       .Generate(IncomingUpdateCount).ToArray()
                                                                                   : Array.Empty<Update>());
    public ValueTask<bool> SetWebHookAsync(string url) => throw new Exception();
    public ValueTask<bool> DeleteWebHookAsync() => throw new Exception();
}

file class TestUpdatesFails : IUpdates
{
    public const int IncomingUpdateCount = 10;
    private readonly Faker<Update> _faker = new();
    public static int Count;
    public ValueTask<Update[]> GetUpdatesAsync(long offset, long limit) => throw new Exception($"test exception {Count++}");
    public ValueTask<bool> SetWebHookAsync(string url) => throw new Exception();
    public ValueTask<bool> DeleteWebHookAsync() => throw new Exception();
}