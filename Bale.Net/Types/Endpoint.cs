namespace Bale.Net.Types;

internal class ApiEndpoint
{
    private readonly string _token;
    public ApiEndpoint(string token)
    {
        _token = token;
    }
    internal string GetUrl(Endpoint endpoint) => $"bot{_token}" + endpoint switch
    {
        Endpoint.GetMe => "/getme",
        _              => ""
    };
}

internal enum Endpoint
{
    GetMe
}