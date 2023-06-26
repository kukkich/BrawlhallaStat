using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Game;

public class Player : IHaveId<int>
{
    public int Id { get; set; }
    //public int InGameId { get; set; }
    public string NickName { get; set; } = null!;
    public Team Team { get; set; }
    public bool IsWinner { get; set; }
    public Customization Customization { get; set; } = null!;
    public LegendDetails Legend { get; set; } = null!;

    public string GameId { get; set; } = null!;
    public Game Game { get; set; } = null!;

    public List<Death> Deaths { get; set; } = null!;
}