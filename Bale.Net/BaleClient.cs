using System.Text;
using System.Text.Json;
using Bale.Net.Implementations;
using Bale.Net.Interfaces;
using Bale.Net.Types;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Polly.Retry;

namespace Bale.Net;

public class BaleClient
{
    public IAttachments Attachments { get; }
    public IChats Chats { get; }
    public IMessages Messages { get; }
    public IPayments Payments { get; }
    public IUpdates Updates { get; }
    public IUsers Users { get; }

    /// <summary>
    /// delay between each retry call
    /// </summary>
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>
    /// maximum retry counts when a call fails
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// a custom action when a retry is happening
    /// </summary>
    public Func<OnRetryArguments<object>, ValueTask>? OnRetry { get; set; }

    /// <summary>
    /// amount of retries that has been attempted so far
    /// </summary>
    public uint RetryAttempts { get; internal set; }

    internal HttpClient HttpClient { get; } //todo : impl retry with polly

    internal readonly string Token;

    internal static readonly Uri BaseUrl = new("https://tapi.bale.ai/", UriKind.Absolute);

    internal readonly ApiEndpoint ApiEndpoint;
    private readonly ResiliencePipeline _pipeline;
    public BaleClient(string token)
    {
        Token = token;
        ApiEndpoint = new ApiEndpoint(token);

        var provider = new ServiceCollection()
            .AddHttpClient(nameof(BaleClient), client => client.BaseAddress = BaseUrl).Services
            .BuildServiceProvider();

        HttpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(BaleClient));

        _pipeline = new ResiliencePipelineBuilder().AddRetry(new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            MaxRetryAttempts = MaxRetryAttempts,
            Delay = Delay,
            OnRetry = arguments =>
            {
                RetryAttempts += 1;
                return OnRetry?.Invoke(arguments) ?? ValueTask.CompletedTask;
            }
        }).Build();

        Users = new Users(this);
        Messages = new Messages(this);
        Updates = new Updates(this);
        Chats = new Chats(this);
        Attachments = new Attachments(this);
        Payments = new Payments(this);
    }
    internal ValueTask<TResponse> TryGetAsync<TResponse>(Endpoint endpoint, string? queryParameter = null) => _pipeline
        .ExecuteAsync(async (parameters, ct)
                          => await GetAsync<TResponse>(parameters.endpoint, parameters.queryParameter, ct), (endpoint, queryParameter));
    internal ValueTask<TResponse> TryPostAsync<TBody, TResponse>(Endpoint endpoint, TBody body, string? queryParameter = null) => _pipeline
        .ExecuteAsync(async (parameters, ct)
                          => await PostAsync<TBody, TResponse>(parameters.endpoint, parameters.body, parameters.queryParameter, ct), (endpoint, body, queryParameter));
    private async ValueTask<TResponse> GetAsync<TResponse>(Endpoint endpoint, string? queryParameter = null, CancellationToken ct = default)
    {
        var url = ApiEndpoint.GetUrl(endpoint);
        if (queryParameter is not null)
            url += queryParameter;

        var response = await HttpClient.GetAsync(url, ct);
        if (response.ReasonPhrase == "Too Many Requests")
            throw new Exception("Rate limit error");

        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<TResponse>>(await response.Content.ReadAsStringAsync(ct));
        if (deserialize is null)
            throw new Exception("Api changed, please contact the author to update the client.");
        if (!deserialize.Ok)
            throw new Exception($"request failed with code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }

    private async ValueTask<TResponse> PostAsync<TBody, TResponse>(Endpoint endpoint, TBody body, string? queryParameter = null, CancellationToken ct = default)
    {
        var url = ApiEndpoint.GetUrl(endpoint);
        if (queryParameter is not null)
            url += queryParameter;

        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync(url, content, ct);
        if (response.ReasonPhrase == "Too Many Requests")
            throw new Exception("Rate limit error");

        var deserialize = JsonSerializer.Deserialize<BaseApiResponse<TResponse>>(await response.Content.ReadAsStringAsync(ct));
        if (deserialize is null)
            throw new Exception("Api changed, please contact the author to update the client.");
        if (!deserialize.Ok)
            throw new Exception($"request failed with code:[{deserialize.ErrorCode}],description:{deserialize.Description}");

        return deserialize.Result;
    }
}