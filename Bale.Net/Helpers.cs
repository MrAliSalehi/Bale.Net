namespace Bale.Net;

internal static class Helpers
{
    public static async ValueTask ExecIgnoreExceptionsAsync(Func<ValueTask> f)
    {
        try
        {
            await f();
        }
        catch
        {
           //ignore
        }
    }
}