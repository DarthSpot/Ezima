using System.ComponentModel.DataAnnotations;

namespace Ezima.API.Model;

public class RewardActivity
{
    public int Id { get; set; }
    [Length(3, 50, ErrorMessage = "Name is too long.")]
    public required string Name { get; set; }
    public string Description { get; set; }
    public int Minutes { get; set; }
}