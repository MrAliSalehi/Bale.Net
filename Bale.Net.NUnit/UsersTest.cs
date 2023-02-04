namespace Bale.Net.NUnit;

public class UsersTest
{
    private readonly BaleClient _client;
    public UsersTest()
    {
        _client = new BaleClient(Helpers.GetTestToken());
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
}