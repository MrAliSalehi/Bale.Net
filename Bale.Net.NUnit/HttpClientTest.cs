namespace Bale.Net.NUnit;

public class HttpClientTest
{
    private readonly BaleClient _client;
    public HttpClientTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }
    [Test]
    public void HttpClient_IsCreatedSuccessfully()
    {
        var httpClient = _client.HttpClient;
        Assert.Multiple(() =>
        {
            Assert.That(_client.HttpClient, Is.Not.Null);
        });
        Assert.That(BaleClient.BaseUrl.AbsolutePath, Is.EqualTo(httpClient.BaseAddress!.AbsolutePath));
    }
}