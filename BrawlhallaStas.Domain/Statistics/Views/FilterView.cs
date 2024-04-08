using BrawlhallaStat.Domain.GameEntities;

namespace BrawlhallaStat.Domain.Statistics.Views;

public class FilterView
{
    public string FilterId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int Wins { get; set; }
    public int Defeats { get; set; }
    public GameType GameType { get; set; }
    public int LegendId { get; set; }
    public int WeaponId { get; set; }
    public int EnemyLegendId { get; set; }
    public int EnemyWeaponId { get; set; }
    public int? TeammateLegendId { get; set; }
    public int? TeammateWeaponId { get; set; }
}