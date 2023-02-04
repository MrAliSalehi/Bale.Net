using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
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

    private readonly ApiEndpoint _apiEndpoint;

    public BaleClient(string token)
    {
        Token = token;
        _apiEndpoint = new(token);

        var provider = new ServiceCollection()
            .AddHttpClient(nameof(BaleClient), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(3);
                client.BaseAddress = BaseUrl;
            }).Services.BuildServiceProvider();


        HttpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(BaleClient));

        Users = new Users(this);
        Messages = new Messages(this);
        Updates = new Updates(this);
        Chats = new Chats(this);
        Attachments = new Attachments();
        Payments = new Payments();
    }
    internal async ValueTask<TResponse> GetAsync<TResponse>(Endpoint endpoint, string? queryParameter = null)
    {
        var url = _apiEndpoint.GetUrl(endpoint);
        if (queryParameter is not null)
            url += queryParameter;
        
        var response = await HttpClient.GetAsync(url);
        if (response.ReasonPhrase == "Too Many Requests")
            throw new Exception("Rate limit error");

        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<TResponse>>(await response.Content.ReadAsStringAsync());
        if (deserialize is null)
            throw new Exception("Api changed, please contact the author to update the client.");
        if (!deserialize.Ok)
            throw new Exception($"request failed with code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }
    internal async ValueTask<TResponse> PostAsync<TBody, TResponse>(Endpoint endpoint, TBody body, string? queryParameter = null)
    {
        var url = _apiEndpoint.GetUrl(endpoint);
        if (queryParameter is not null)
            url += queryParameter;

        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync(url, content);
        if (response.ReasonPhrase == "Too Many Requests")
            throw new Exception("Rate limit error");

        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<TResponse>>(await response.Content.ReadAsStringAsync());
        if (deserialize is null)
            throw new Exception("Api changed, please contact the author to update the client.");
        if (!deserialize.Ok)
            throw new Exception($"request failed with code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }
}