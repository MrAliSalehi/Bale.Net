using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IUsers
{
    ValueTask<User> GetMeAsync();
}