namespace Bale.Net.NUnit.InterfaceTests;

public class UpdatesTest
{
    private readonly BaleClient _client;
    private const long MyId = 2047754943;
    public UpdatesTest()
    {
        _client = new(Helpers.GetTestToken());
    }
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
        var updates = await _client.Updates.GetUpdatesAsync(1, 0);


        var update = updates.First();

        Assert.That(updates, Is.Not.Empty.Or.Null);
        Assert.That(updates.Length, Is.Positive.Within(5));
        
        Assert.That(update.Message, Is.Not.Null);
        Assert.That(update.Message!.Chat, Is.Not.Null);
        Assert.That(update.Message!.From, Is.Not.Null);

        Assert.That(update.Message.From!.Id, Is.EqualTo(MyId));
        Assert.That(update.Message.Chat!.Id, Is.EqualTo(MyId));
    }
}