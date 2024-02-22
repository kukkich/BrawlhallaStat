using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.GameEntities;

public class Death : IHaveId<int>
{
    public int Id { get; set; }
    public int TimeStamp { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int GameId { get; set; }
    public GameDetail GameDetail { get; set; } = null!;
}