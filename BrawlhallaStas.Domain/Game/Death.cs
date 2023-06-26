using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Game;

public class Death : IHaveId<int>
{
    public int Id { get; set; }
    public int TimeStamp { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;
}