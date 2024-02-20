namespace BrawlhallaStat.Domain.Games.Views;

public class GameStatisticView
{
    public string UserId { get; set; } = null!;
    public string GameDetailId { get; set; } = null!;
    public GameType GameType { get; set; }
    public bool IsWin { get; set; }
    public int LegendId { get; set; }
    public int WeaponId { get; set; }
    public int EnemyLegendId { get; set; }
    public int EnemyWeaponId { get; set; }
    public int? TeammateLegendId { get; set; }
    public int? TeammateWeaponId { get; set; }
}