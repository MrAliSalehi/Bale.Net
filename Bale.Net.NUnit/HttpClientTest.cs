namespace Bale.Net.NUnit;

public class HttpClientTest
{
    private readonly BaleClient _client;
    public HttpClientTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }
    [Test]
    public void HttpClientFactory_IsCreatedSuccessfully()
    {
        var httpClient = _client.HttpClientFactory.CreateClient("baleApi");
        Assert.Multiple(() =>
        {
            Assert.That(_client.HttpClientFactory, Is.Not.Null);
            Assert.That(httpClient, Is.Not.Null);
        });
        Assert.That(BaleClient.BaseUrl.AbsolutePath, Is.EqualTo(httpClient.BaseAddress!.AbsolutePath));
    }
}