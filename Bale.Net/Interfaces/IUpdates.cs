using System.Diagnostics.CodeAnalysis;
using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IUpdates
{
    ValueTask<bool> SetWebHookAsync([StringSyntax(StringSyntaxAttribute.Uri)]string url);
    ValueTask<bool> DeleteWebHookAsync();
    ValueTask<Update[]> GetUpdatesAsync(long offset,long limit);
}