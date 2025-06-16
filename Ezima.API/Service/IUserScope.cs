using Ezima.API.Model;
using Ezima.API.Model.Request;

namespace Ezima.API.Service;

public interface IUserScope
{
    bool HasAccessToChild(int childId);
    int? UserId { get; }
    bool HasAccessToData(RewardUsage usage);
    bool HasAccessToData(Reward reward);
}

public class ApiUserScope : IUserScope 
{
    public User User { get; }
    public int? UserId => User.Id;

    private List<int> childIds = [];
    
    public ApiUserScope(User user)
    {
        User = user;
        childIds.AddRange(user.Children.Select(x => x.Id).ToList());
    }

    public bool HasAccessToChild(int childId)
    {
        return childIds.Contains(childId);
    }
    
    public bool HasAccessToData(RewardUsage usage)
    {
        return usage.Children.Any(x => childIds.Contains(x.Id));
    }

    public bool HasAccessToData(Reward reward)
    {
        return reward.Children.Any(x => childIds.Contains(x.Id));
    }
}

public class InvalidUserScope : IUserScope
{
    public int? UserId => null;
    public bool HasAccessToData(RewardUsage usage)
    {
        return false;
    }

    public bool HasAccessToData(Reward reward)
    {
        return false;
    }

    public bool HasAccessToChild(int childId)
    {
        return false;
    }

}