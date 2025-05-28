using System.Text.Json.Serialization;

namespace Ezima.API.Model;

public class Reward
{
    public int Id { get; set; }
    public DateTime IssuedOn { get; set; }
    public RewardActivity? Activity { get; set; } 
    public string Comment { get; set; }
    public int DefaultMinutes { get; set; }
}