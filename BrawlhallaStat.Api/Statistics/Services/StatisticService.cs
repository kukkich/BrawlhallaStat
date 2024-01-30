using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Base;
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

    public async Task<Statistic> GetStatisticsAsync(StatisticGeneralFilter filter, IUserIdentity user)
    {
        var filteredGames = _dbContext.GameStatistics
            .Where(x => x.UserId == user.Id)
            .ApplyFilter(filter)
            .Select(x => new {x.GameDetailId, x.IsWin})
            .Distinct();

        return new Statistic
        {
            Wins = await filteredGames.Where(x => x.IsWin)
                .CountAsync(),
            Defeats = await filteredGames.Where(x => !x.IsWin)
                .CountAsync(),
        };
    }
}