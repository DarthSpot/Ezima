using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Model.Context;

public class EzimaContext : DbContext
{
    public DbSet<Child> Children { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<RewardActivity> RewardActivities { get; set; }
    public DbSet<RewardUsage> RewardUsages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
    }
}