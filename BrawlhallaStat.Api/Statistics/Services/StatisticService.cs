using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Statistics.Services;

public class StatisticService : IStatisticService
{
    private readonly BrawlhallaStatContext _dbContext;

    public StatisticService(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Statistic> GetStatisticsAsync(StatisticGeneralFilter filter)
    {
        var filteredGames = filter.ApplyFilterExpression(_dbContext.GameStatistics.AsNoTracking())
            .DistinctBy(x => x.GameDetailId);

        return new Statistic
        {
            Wins = await filteredGames.Where(x => x.IsWin)
                .CountAsync(),
            Defeats = await filteredGames.Where(x => !x.IsWin)
                .CountAsync(),
        };
    }
}