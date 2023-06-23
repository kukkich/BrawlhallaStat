using BrawlhallaStat.Domain.Statistics;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Context;

public class BrawlhallaStatContext : DbContext
{
    public DbSet<Legend> Legends { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<WeaponStatistic> WeaponStatistics { get; set; }
    public DbSet<LegendStatistic> LegendStatistics { get; set; }
    public DbSet<WeaponAgainstLegendStatistic> WeaponAgainstLegendStatistics { get; set; }
    public DbSet<WeaponAgainstWeaponStatistic> WeaponAgainstWeaponStatistics { get; set; }
    public DbSet<LegendAgainstWeaponStatistic> LegendAgainstWeaponStatistics { get; set; }
    public DbSet<LegendAgainstLegendStatistic> LegendAgainstLegendStatistics { get; set; }

    public BrawlhallaStatContext(DbContextOptions options)
        : base(options)
    {
        //Database.EnsureDeleted();
        if (!Database.EnsureCreated()) return;
    }
}