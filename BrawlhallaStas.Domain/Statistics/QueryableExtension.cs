using BrawlhallaStat.Domain.GameEntities.Views;

namespace BrawlhallaStat.Domain.Statistics;

public static class QueryableExtension
{
    public static IQueryable<GameStatisticView> ApplyFilter(this IQueryable<GameStatisticView> source, StatisticFilterBase filter)
    {
        return filter.ApplyFilterExpression(source);
    }
}