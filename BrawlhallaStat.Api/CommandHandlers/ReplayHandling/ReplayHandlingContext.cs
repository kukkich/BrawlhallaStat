using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling;

public class ReplayHandlingContext
{
    public IUserIdentity ReplayAuthor { get; set; } = null!;
    public User UserFromDb { get; set; } = null!;
    public Player UserFromGame { get; set; } = null!;
    public GameDetail GameDetail { get; set; } = null!;
    public Legend Legend { get; set; } = null!;
    public Weapon[] Weapons { get; set; } = null!;

    public Legend[] OpponentLegends { get; set; } = null!;
    public Weapon[] OpponentWeapons { get; set; } = null!;
}