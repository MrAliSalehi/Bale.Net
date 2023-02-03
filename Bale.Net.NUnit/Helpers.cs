using Microsoft.Extensions.Configuration;

namespace Bale.Net.NUnit;

public static class Helpers
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .AddJsonFile("secrets.json")
        .Build();

    internal static string GetTestToken()
    {
        return Configuration.GetRequiredSection("Token").Value!;
    }
}