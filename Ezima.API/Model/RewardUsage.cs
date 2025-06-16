using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Model;

public class RewardUsage
{
    [Key]
    public int Id { get; set; }
    public DateTime UsedOn { get; set; }
    public int Minutes { get; set; }
    public string Comment { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual List<Child> Children { get; set; } = [];
}