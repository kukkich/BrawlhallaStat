using BrawlhallaStat.Domain.GameEntities.Views;
using BrawlhallaStat.Domain.GameEntities;
using System.Linq.Expressions;

namespace BrawlhallaStat.Domain.Statistics;

public abstract class StatisticFilterBase
{
    // Todo build expression in runtime, see https://www.youtube.com/watch?v=0hDCDmfuiFY
    public static readonly Expression<Func<GameFilter, bool>> GameMather = gameFilter =>
        (gameFilter.Filter.GameType == null || gameFilter.Filter.GameType == gameFilter.Game.GameType) &&
        (gameFilter.Filter.LegendId == null || gameFilter.Filter.LegendId == gameFilter.Game.LegendId) &&
        (gameFilter.Filter.WeaponId == null || gameFilter.Filter.WeaponId == gameFilter.Game.WeaponId) &&
        (gameFilter.Filter.EnemyLegendId == null || gameFilter.Filter.EnemyLegendId == gameFilter.Game.EnemyLegendId) &&
        (gameFilter.Filter.EnemyWeaponId == null || gameFilter.Filter.EnemyWeaponId == gameFilter.Game.EnemyWeaponId) &&
        (gameFilter.Filter.TeammateLegendId == null || gameFilter.Filter.TeammateLegendId == gameFilter.Game.TeammateLegendId) &&
        (gameFilter.Filter.TeammateWeaponId == null || gameFilter.Filter.TeammateWeaponId == gameFilter.Game.TeammateWeaponId);

    public GameType? GameType { get; set; }
    public int? LegendId { get; set; }
    public int? WeaponId { get; set; }
    public int? EnemyLegendId { get; set; }
    public int? EnemyWeaponId { get; set; }
    public int? TeammateLegendId { get; set; }
    public int? TeammateWeaponId { get; set; }

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
            GameType is not (
                null or
                GameEntities.GameType.Ranked2V2 or
                GameEntities.GameType.Unranked2V2
            ))
        {
            return false;
        }

        return true;
    }

    public Expression<Func<StatisticFilterBase, bool>> GetEqualityComparer()
    {
        return filter => GameType == filter.GameType &&
                                LegendId == filter.LegendId &&
                                WeaponId == filter.WeaponId &&
                                EnemyLegendId == filter.EnemyLegendId &&
                                EnemyWeaponId == filter.EnemyWeaponId &&
                                TeammateLegendId == filter.TeammateLegendId &&
                                TeammateWeaponId == filter.TeammateWeaponId;
    }

    public IQueryable<GameStatisticView> ApplyFilterExpression(IQueryable<GameStatisticView> source)
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

    private static IQueryable<GameStatisticView> ApplyPropertyFilterIfNotNull<TValue>(
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
}