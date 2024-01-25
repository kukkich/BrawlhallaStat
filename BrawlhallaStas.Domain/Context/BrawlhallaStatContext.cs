using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Context;

public class BrawlhallaStatContext : DbContext
{
    public DbSet<Legend> Legends { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Game> Games { get; set; }
    public DbSet<ReplayFile> ReplayFiles { get; set; }
    public DbSet<Death> Deaths { get; set; }
    public DbSet<GameDetail> GameDetails { get; set; }
    public DbSet<Player> Players { get; set; }

    public DbSet<Token> Tokens { get; set; }
    public DbSet<IdentityClaim> Claims { get; set; }
    public DbSet<Role> Roles { get; set; }

    public BrawlhallaStatContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureDeleted();
        if (!Database.EnsureCreated()) return;
    }
}