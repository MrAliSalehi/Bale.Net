using System.Diagnostics.CodeAnalysis;

namespace Bale.Net.Interfaces;

public interface IUpdates
{
    ValueTask<bool> SetWebHookAsync([StringSyntax(StringSyntaxAttribute.Uri)]string url);
    ValueTask<bool> DeleteWebHookAsync();
}