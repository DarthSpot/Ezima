using Ezima.API.Model;
using Ezima.API.Repository;

namespace Ezima.API.Service;

public class AuthScopeService : IAuthScopeService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserRepository _userRepository;

    public AuthScopeService(IHttpContextAccessor accessor, UserRepository userRepository)
    {
        _accessor = accessor;
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync()
    {
        var claimMap = _accessor?.HttpContext?.User?.Claims?
            .ToLookup(x => x.Type, x => x.Value)
            .ToDictionary(x => x.Key, x => x.ToList());
        if (claimMap == null)
            return null;
        return claimMap.TryGetValue(EzimaSchema.EZIMA_USER_ID, out var userId) && 
               int.TryParse(userId.Single(), out var id)
            ? await _userRepository.FindById(id)
            : null;
    }

    public async Task<IUserScope> GetUserScopeAsync()
    {
        var user = await GetUserAsync();
        if (user == null)
            return new InvalidUserScope();
        return new ApiUserScope(user);
    }
}