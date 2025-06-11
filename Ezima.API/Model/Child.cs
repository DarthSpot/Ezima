using System.ComponentModel.DataAnnotations;

namespace Ezima.API.Model;

public class Child
{
    public int Id { get; set; }
    [Length(3, 50, ErrorMessage = "Name is too long.")]
    public string Name { get; set; } = string.Empty;
    public required DateTime Birthday { get; set; }
    public int RewardTime { get; set; }
    public virtual List<Reward> Rewards { get; set; } = [];
    public virtual List<RewardActivity> RewardActivities { get; set; } = [];
    public virtual List<RewardUsage> RewardUsages { get; set; } = [];
    public virtual List<User> Parents { get; set; } = [];
}