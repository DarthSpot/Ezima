namespace Ezima.API.Model.Request;

public class RewardRequest : IEntityRequest<Reward>
{
    public int? ActivityId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Minutes { get; set; }
    
    public DateTime? IssuedOn { get; set; }
    
    public Reward ToEntity()
    {
        return new Reward()
        {
            Comment = Comment,
            DefaultMinutes = Minutes,
            IssuedOn = IssuedOn?.ToUniversalTime() ?? DateTime.UtcNow
        };
    }
}