namespace Bale.Net;

internal class ApiEndpoint
{
    private readonly string _token;
    public ApiEndpoint(string token)
    {
        _token = token;
    }
    internal string GetUrl(Endpoint endpoint) => $"/bot{_token}" + endpoint switch
    {
        Endpoint.GetMe         => "/getme",
        Endpoint.SendMessage   => "/sendMessage",
        Endpoint.EditMessage   => "/EditMessageText",
        Endpoint.DeleteMessage => "/deleteMessage",
        Endpoint.SetWebHook    => "/setWebhook",
        Endpoint.DeleteWebHook => "/deleteWebhook",
        Endpoint.GetUpdates    => "/getUpdates",
        Endpoint.GetChat       => "/getchat",
        _                      => ""
    };
}

internal enum Endpoint
{
    GetMe,
    SendMessage,
    EditMessage,
    DeleteMessage,
    SetWebHook,
    DeleteWebHook,
    GetUpdates,
    GetChat,
}