using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Views;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Views;
using Microsoft.EntityFrameworkCore;
// ReSharper disable VirtualMemberCallInConstructor

namespace BrawlhallaStat.Domain.Context;

public class BrawlhallaStatContext : DbContext
{
    public DbSet<Legend> Legends { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<ReplayFile> ReplayFiles { get; set; } = null!;
    public DbSet<Death> Deaths { get; set; } = null!;
    public DbSet<GameDetail> GameDetails { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;

    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<IdentityClaim> Claims { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public DbSet<StatisticFilter> StatisticFilters { get; set; }
    public DbSet<GameStatisticView> GameStatistics { get; set; } = null!;
    public DbSet<FilterView> FiltersView { get; set; } = null!;
    
    public BrawlhallaStatContext(DbContextOptions options)
        : base(options)
    {
        //Database.EnsureDeleted();
        // if (!Database.EnsureCreated()) return;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<FilterView>()
            .ToView("FilterView")
            .HasNoKey();

        modelBuilder
            .Entity<GameStatisticView>()
            .ToView("GameStatisticView")
            .HasNoKey();
    }
}