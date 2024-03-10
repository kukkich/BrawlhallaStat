using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Statistics.Services;

public class StatisticService : IStatisticService
{
    private readonly BrawlhallaStatContext _dbContext;

    public StatisticService(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Statistic> GetStatistic(StatisticFilterDto filter, IUserIdentity user)
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

    public Task<IEnumerable<StatisticWithFilter>> GetStatisticsFromUserFilters(IUserIdentity user)
    {
        throw new NotImplementedException();
    }

    public Task<StatisticWithFilter> AddFilter(StatisticFilterDto filter, IUserIdentity user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFilter(string id, IUserIdentity user)
    {
        throw new NotImplementedException();
    }
}