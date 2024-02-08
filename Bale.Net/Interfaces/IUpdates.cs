using System.Diagnostics.CodeAnalysis;
using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IUpdates
{
    /// <summary>
    /// set web hook
    /// </summary>
    /// <param name="url">webhook url</param>
    /// <returns>if the call was successful</returns>
    ValueTask<bool> SetWebHookAsync([StringSyntax(StringSyntaxAttribute.Uri)]string url);
    /// <summary>
    /// delete the last webhook that was set
    /// </summary>
    /// <returns>true if the webhook was deleted successfully</returns>
    ValueTask<bool> DeleteWebHookAsync();
    /// <summary>
    /// get updates
    /// </summary>
    /// <param name="offset">offset used to paginate the updates</param>
    /// <param name="limit">limit the number of the updates returned by the api</param>
    /// <returns>all the available updates</returns>
    ValueTask<Update[]> GetUpdatesAsync(long offset,long limit);
}