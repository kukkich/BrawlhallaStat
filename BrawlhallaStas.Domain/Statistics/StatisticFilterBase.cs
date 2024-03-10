using BrawlhallaStat.Domain.GameEntities.Views;
using BrawlhallaStat.Domain.GameEntities;
using System.Linq.Expressions;

namespace BrawlhallaStat.Domain.Statistics;

public abstract class StatisticFilterBase
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
        source = ApplyPropertyFilterIfNotNull(source, GameType, game => game.GameType == GameType);
        source = ApplyPropertyFilterIfNotNull(source, LegendId, game => game.LegendId == LegendId);
        source = ApplyPropertyFilterIfNotNull(source, WeaponId, game => game.WeaponId == WeaponId);
        source = ApplyPropertyFilterIfNotNull(source, EnemyLegendId, game => game.EnemyLegendId == EnemyLegendId);
        source = ApplyPropertyFilterIfNotNull(source, EnemyWeaponId, game => game.EnemyWeaponId == EnemyWeaponId);
        source = ApplyPropertyFilterIfNotNull(source, TeammateLegendId, game => game.TeammateLegendId == TeammateLegendId);
        source = ApplyPropertyFilterIfNotNull(source, TeammateWeaponId, game => game.TeammateWeaponId == TeammateWeaponId);

        return source;
    }

    private IQueryable<GameStatisticView> ApplyPropertyFilterIfNotNull<TValue>(
        IQueryable<GameStatisticView> source,
        TValue? value,
        Expression<Func<GameStatisticView, bool>> predicate
    ) where TValue : struct
    {
        if (value is not null)
        {
            source = source.Where(predicate);
        }

        return source;
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
            GameType is not (GameEntities.GameType.Ranked2V2 or GameEntities.GameType.Unranked2V2))
        {
            return false;
        }

        return true;
    }
}