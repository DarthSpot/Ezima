namespace Ezima.API.Model.Request;

public class RewardUsageRequest : IEntityRequest<RewardUsage>
{
    public int Minutes { get; set; }
    public string Comment { get; set; } = string.Empty;
    
    public DateTime? UsedOn { get; set; }
    
    public List<int> ChildrenIds { get; set; } = [];
    
    
    public RewardUsage ToEntity()
    {
        return new RewardUsage()
        {
            Minutes = Minutes,
            Comment = Comment,
            UsedOn = UsedOn?.ToUniversalTime() ?? DateTime.UtcNow
        };
    }
}