using System.Text.Json;
using Bale.Net.Implementations;
using Bale.Net.Interfaces;
using Bale.Net.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Bale.Net;

public class BaleClient
{
    public IAttachments Attachments { get; }
    public IChats Chats { get; }
    public IMessages Messages { get; }
    public IPayments Payments { get; }
    public IUpdates Updates { get; }
    public IUsers Users { get; }
    public HttpClient HttpClient { get; } //todo : impl retry with polly

    internal readonly string Token;

    internal static readonly Uri BaseUrl = new("https://tapi.bale.ai/", UriKind.Absolute);

    internal readonly ApiEndpoint ApiEndpoint;

    public BaleClient(string token)
    {
        Token = token;
        ApiEndpoint = new(token);

        var provider = new ServiceCollection()
            .AddHttpClient(nameof(BaleClient), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(3);
                client.BaseAddress = BaseUrl;
            }).Services.BuildServiceProvider();


        HttpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(BaleClient));

        Attachments = new Attachments();
        Chats = new Chats();
        Messages = new Messages();
        Payments = new Payments();
        Updates = new Updates();
        Users = new Users(this);
    }
    internal async ValueTask<TResponse> GetAsync<TResponse>(Endpoint endpoint, string? queryParameter = null)
    {
        var url = ApiEndpoint.GetUrl(endpoint);
        if (queryParameter is not null)
            url += queryParameter;

        var response = await HttpClient.GetAsync(url);
        if (response.ReasonPhrase == "Too Many Requests")
            throw new Exception("Rate limit error");

        var content = await response.Content.ReadAsStringAsync();
        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<TResponse>>(content);
        if (deserialize is null)
            throw new Exception("Api changed, please contact the author to update the client.");
        if (!deserialize.Ok)
            throw new Exception($"request failed with code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }
}