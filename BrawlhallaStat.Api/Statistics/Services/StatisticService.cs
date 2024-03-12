using AutoMapper;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Statistics.Services;

public class StatisticService : IStatisticService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;

    public StatisticService(BrawlhallaStatContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Statistic> GetStatistic(StatisticFilterCreateDto filterCreate, IUserIdentity user)
    {
        var filteredGames = _dbContext.GameStatistics
            .Where(x => x.UserId == user.Id)
            .ApplyFilter(filterCreate)
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

    public async Task<StatisticWithFilter> AddFilter(StatisticFilterCreateDto filter, IUserIdentity user)
    {
        var newFilter = _mapper.Map<StatisticFilter>(filter);
        newFilter.UserId = user.Id;
        newFilter.Id = Guid.NewGuid().ToString();

        _dbContext.StatisticFilters.Add(newFilter);
        await _dbContext.SaveChangesAsync();

        var filteredGames = _dbContext.GameStatistics
            .ApplyFilter(newFilter)
            .Select(x => new {x.GameDetailId, x.IsWin})
            .Distinct();
        var statistic = new Statistic
        {
            Wins = await filteredGames.Where(x => x.IsWin)
                .CountAsync(),
            Defeats = await filteredGames.Where(x => !x.IsWin)
                .CountAsync()
        };

        return new StatisticWithFilter
        {
            Statistic = statistic,
            Filter = _mapper.Map<StatisticFilterPublicDto>(newFilter)
        };
    }

    public Task DeleteFilter(string id, IUserIdentity user)
    {
        throw new NotImplementedException();
    }
}