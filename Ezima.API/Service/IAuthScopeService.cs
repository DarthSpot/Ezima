using Ezima.API.Model;

namespace Ezima.API.Service;

public interface IAuthScopeService
{
    public Task<User> GetUserAsync();
    public Task<IUserScope> GetUserScopeAsync();
}