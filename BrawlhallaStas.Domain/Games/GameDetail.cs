using System.ComponentModel.DataAnnotations.Schema;
using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Games;

public class GameDetail : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public GameType Type { get; set; }
    public int RandomSeed { get; set; }
    public int Version { get; set; }
    public bool OnlineGame { get; set; }
    public int LevelId { get; set; }
    public int EndOfMatchFanfareId { get; set; }
    public string PlaylistName { get; set; } = null!;
    public int AuthorPlayerId { get; set; }
    public Player AuthorPlayer { get; set; }

    public List<Player> Players { get; set; } = null!;
    [NotMapped]
    public IEnumerable<Player> Winners => Players.Where(x => x.IsWinner);
    [NotMapped]
    public IEnumerable<Player> Losers => Players.Where(x => !x.IsWinner);
    public List<Death> Deaths { get; set; } = null!;

    public GameSettings Settings { get; set; } = null!;
}