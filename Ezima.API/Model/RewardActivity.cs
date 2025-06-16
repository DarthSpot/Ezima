using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ezima.API.Model;

public class RewardActivity
{
    public int Id { get; set; }
    [Length(3, 50, ErrorMessage = "Name is too long.")]
    public required string Name { get; set; }
    public string Description { get; set; }
    public int DefaultMinutes { get; set; }
    [JsonIgnore]
    public virtual List<Child> Children { get; set; } = [];
}