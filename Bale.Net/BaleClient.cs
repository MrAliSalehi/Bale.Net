using Bale.Net.Implementations;
using Bale.Net.Interfaces;
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
    public IHttpClientFactory HttpClientFactory { get; set; }

    internal readonly string Token;

    internal static readonly Uri BaseUrl = new("https://tapi.bale.ai/", UriKind.Absolute);
    public BaleClient(string token)
    {
        Token = token;
        Attachments = new Attachments();
        Chats = new Chats();
        Messages = new Messages();
        Payments = new Payments();
        Updates = new Updates();
        Users = new Users();

        var collection = new ServiceCollection();

        collection.AddHttpClient("baleApi", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(3);
            client.BaseAddress = BaseUrl;
        });

        var provider = collection.BuildServiceProvider();

        HttpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    }
}