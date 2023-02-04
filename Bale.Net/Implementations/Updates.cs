using System.Diagnostics.CodeAnalysis;
using Bale.Net.Interfaces;
using Bale.Net.Types;
using Bale.Net.Types.Internal;

namespace Bale.Net.Implementations;

public class Updates : IUpdates
{
    private readonly BaleClient _client;
    public Updates(BaleClient client)
    {
        _client = client;
    }

    public async ValueTask<bool> SetWebHookAsync([StringSyntax(StringSyntaxAttribute.Uri)] string url)
    {
        var body = new SetWebhookRequest { Url = url };
        return await _client.PostAsync<SetWebhookRequest, bool>(Endpoint.SetWebHook, body);
    }
    public async ValueTask<bool> DeleteWebHookAsync()
    {
        return await _client.GetAsync<bool>(Endpoint.DeleteWebHook);
    }
    public async ValueTask<Update[]> GetUpdatesAsync(long offset, long limit)
    {
        var body = new GetUpdatesRequest
        {
            Limit = limit, Offset = offset
        };
        var response = await _client.PostAsync<GetUpdatesRequest, Update[]>(Endpoint.GetUpdates, body);
        return !response.Any() ? Array.Empty<Update>() : response;
    }
}