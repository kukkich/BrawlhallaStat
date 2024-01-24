using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Games;

public class Player : IHaveId<int>
{
    public int Id { get; set; }
    public string NickName { get; set; } = null!;
    public Team Team { get; set; }
    public bool IsWinner { get; set; }
    public Customization Customization { get; set; } = null!;
    public LegendDetails LegendDetails { get; set; } = null!;

    public string GameDetailId { get; set; } = null!;
    public GameDetail GameDetail { get; set; } = null!;

    public List<Death> Deaths { get; set; } = null!;
}