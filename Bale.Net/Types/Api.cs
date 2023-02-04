namespace Bale.Net.Types;

public static class Api
{
    public static Dictionary<Endpoint,string> Endpoints { get; set; } = new ()
    {
        { Endpoint.GetMe ,"/getme"}
    };
    
}

public enum Endpoint
{
    GetMe
}