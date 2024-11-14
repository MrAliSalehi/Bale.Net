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

    public async ValueTask<bool> SetWebHookAsync([StringSyntax(StringSyntaxAttribute.Uri)] string url) => await _client.TryPostAsync<SetWebhookRequest, bool>(Endpoint.SetWebHook, new SetWebhookRequest { Url = url });
    public async ValueTask<bool> DeleteWebHookAsync() => await _client.TryGetAsync<bool>(Endpoint.DeleteWebHook);
    public async ValueTask<Update[]> GetUpdatesAsync(long offset = 0, long limit = 0) =>
        await _client.TryPostAsync<GetUpdatesRequest, Update[]>(Endpoint.GetUpdates, new GetUpdatesRequest
        {
            Limit = limit, Offset = offset
        });
}