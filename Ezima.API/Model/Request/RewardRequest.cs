namespace Ezima.API.Model.Request;

public class RewardRequest : IEntityRequest<Reward>
{
    public int? ActivityId { get; set; }
    public int Minutes { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime? IssuedOn { get; set; }
    public List<int> ChildrenIds { get; set; } = [];
    
    public Reward ToEntity()
    {
        return new Reward()
        {
            Comment = Comment,
            Minutes = Minutes,
            IssuedOn = IssuedOn?.ToUniversalTime() ?? DateTime.UtcNow
        };
    }
}