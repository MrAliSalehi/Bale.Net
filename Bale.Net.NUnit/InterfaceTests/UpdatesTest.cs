namespace Bale.Net.NUnit.InterfaceTests;

public class UpdatesTest
{
    private readonly BaleClient _client;
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
}