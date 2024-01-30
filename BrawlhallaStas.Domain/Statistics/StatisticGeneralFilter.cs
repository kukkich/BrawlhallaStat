using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Games.Views;

namespace BrawlhallaStat.Domain.Statistics;

public class StatisticGeneralFilter
{
    public GameType? GameType { get; set; }
    public int? LegendId { get; set; }
    public int? WeaponId { get; set; }
    public int? EnemyLegendId { get; set; }
    public int? EnemyWeaponId { get; set; }
    public int? TeammateLegendId { get; set; }
    public int? TeammateWeaponId { get; set; }

    internal IQueryable<GameStatisticView> ApplyFilterExpression(IQueryable<GameStatisticView> source)
    {
        var result = source;
        if (GameType is not null)
        {
            result = result.Where(x => x.GameType == GameType);
        }
        if (LegendId is not null)
        {
            result = result.Where(x => x.LegendId == LegendId);
        }
        if (WeaponId is not null)
        {
            result = result.Where(x => x.WeaponId == WeaponId);
        }
        if (EnemyLegendId is not null)
        {
            result = result.Where(x => x.EnemyLegendId == EnemyLegendId);
        }
        if (EnemyWeaponId is not null)
        {
            result = result.Where(x => x.EnemyWeaponId == EnemyWeaponId);
        }
        if (TeammateLegendId is not null)
        {
            result = result.Where(x => x.TeammateLegendId == TeammateLegendId);
        }
        if (TeammateWeaponId is not null)
        {
            result = result.Where(x => x.TeammateWeaponId == TeammateWeaponId);
        }

        return result;
    }

    public bool IsValid()
    {
        if (LegendId is not null && WeaponId is not null)
        {
            return false;
        }
        if (EnemyLegendId is not null && EnemyWeaponId is not null)
        {
            return false;
        }
        if (TeammateLegendId is not null && TeammateWeaponId is not null)
        {
            return false;
        }

        if ((TeammateLegendId is not null || TeammateWeaponId is not null) &&
            GameType is not (Games.GameType.Ranked2V2 or Games.GameType.Unranked2V2))
        {
            return false;
        }

        return true;
    }
}