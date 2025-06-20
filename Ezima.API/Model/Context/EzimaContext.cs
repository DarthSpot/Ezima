using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Model.Context;

public class EzimaContext : DbContext
{
    public DbSet<Child> Children { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<RewardActivity> RewardActivities { get; set; }
    public DbSet<RewardUsage> RewardUsages { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Children)
            .WithMany(e => e.Parents);
        modelBuilder.Entity<Child>()
            .HasMany(e => e.Rewards)
            .WithMany(e => e.Children);
        modelBuilder.Entity<Child>()
            .HasMany(e => e.RewardUsages)
            .WithMany(e => e.Children);
        modelBuilder.Entity<Child>()
            .HasMany(e => e.RewardActivities)
            .WithMany(e => e.Children);
    }
}