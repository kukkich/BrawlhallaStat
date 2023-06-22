using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Context;

public class BrawlhallaStatContext : DbContext
{
    public DbSet<Legend> Legends { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public BrawlhallaStatContext(DbContextOptions options)
        : base(options)
    {
        if (!Database.EnsureCreated()) return;
    }
}