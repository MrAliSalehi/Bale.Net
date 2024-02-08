namespace Bale.Net.NUnit;

public class ApiEndpointTests
{
    private readonly ApiEndpoint _apiEndpoint = new("some_token");
    [Test]
    public void ApiEndpoint_Should_throw()
    {
        Assert.Throws<ArgumentException>(() => _apiEndpoint.GetUrl(Endpoint.None));
    }

    [Test]
    public void ApiEndpoint_Should_Map_Correctly()
    {
        Assert.True(_apiEndpoint.GetUrl(Endpoint.SendInvoice) is "/botsome_token/SendInvoice");
    }
}