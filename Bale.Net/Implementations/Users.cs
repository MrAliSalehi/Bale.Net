using Bale.Net.Interfaces;
using Bale.Net.Types;

namespace Bale.Net.Implementations;

public class Users : IUsers
{
    private readonly BaleClient _client;
    internal Users(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<User> GetMeAsync()
    {
        var response = await _client.GetAsync<User>(Endpoint.GetMe);

        if (response.Id == 0)
            throw new Exception("invalid token, request failed.");

        return response;
    }
}