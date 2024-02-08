using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IUsers
{
    /// <summary>
    /// get me
    /// </summary>
    /// <returns>current bot</returns>
    ValueTask<User> GetMeAsync();
}