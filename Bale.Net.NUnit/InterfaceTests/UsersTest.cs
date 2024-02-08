namespace Bale.Net.NUnit.InterfaceTests;

public class UsersTest
{
    private readonly BaleClient _client;
    public UsersTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
    }
    [Test]
    public void IUser_IsNotNull()
    {
        Assert.That(_client.Users,Is.Not.Null);
    }
    [Test]
    public async Task GetMe_ShouldReturnMe()
    {
        var me = await _client.Users.GetMeAsync();
        Assert.Multiple(() =>
        {
            Assert.That(me, Is.Not.Null);
            Assert.That(me.Username, Is.EqualTo("qwxptestbot"));
            Assert.That(me.Id, Is.Not.Zero);
        });
    }

    [Test]
    public void GetMe_ShouldThrow()
    {
        var cl = new BaleClient("invalid token");
        Assert.ThrowsAsync<Exception>(async ()=>await cl.Users.GetMeAsync());
    }
}