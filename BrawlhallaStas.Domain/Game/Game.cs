using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Game;

public class Game : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public GameType Type { get; set; }
    public int RandomSeed { get; set; }
    public int Version { get; set; }
    public bool OnlineGame { get; set; }
    public int LevelId { get; set; }
    public int EndOfMatchFanfare { get; set; }
    public string PlaylistName { get; set; } = null!;

    public List<Player> Players { get; set; } = null!;
    public IEnumerable<Player> Winners => Players.Where(x => x.IsWinner);
    public IEnumerable<Player> Losers => Players.Where(x => !x.IsWinner);
    public List<Death> Deaths { get; set; } = null!;

    public GameSettings Settings { get; set; } = null!;
}