using Microsoft.Extensions.Configuration;
using NUnit.Framework.Internal;

namespace Bale.Net.NUnit;

public class TokenTest
{
    private readonly string _token;
    private BaleClient? _client;
    public TokenTest()
    {
        _token = Helpers.GetTestToken();
    }

    [Test]
    public void TestToken_IsNotNull()
    {
        Assert.That(_token, Is.Not.Null);
    }
    [Test]
    public void ClientToken_IsNotNull()
    {
        _client = new(_token);
        Assert.That(_client.Token, Is.Not.Null);
    }

    [Test]
    public void GetMe_ShouldThrow_WhenTokenInvalid()
    {
        _client = new("some invalid token");
        Assert.ThrowsAsync<Exception>(async () => await _client.Users.GetMeAsync());
    }
}