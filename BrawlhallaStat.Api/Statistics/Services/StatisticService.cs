using System.Linq.Expressions;
using AutoMapper;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Api.General.Time;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities.Views;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BrawlhallaStat.Api.Statistics.Services;

public class StatisticService : IStatisticService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ITimeProvider _timeProvider;

    public StatisticService(BrawlhallaStatContext dbContext, IMapper mapper, ITimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _timeProvider = timeProvider;
    }

    public async Task<Statistic> GetStatistic(StatisticFilterCreateDto filter, IUserIdentity user)
    {
        var statistic = await _dbContext.GameStatistics
            .Where(x => x.UserId == user.Id)
            .ApplyFilter(filter)
            .Select(x => new { x.GameDetailId, x.IsWin })
            .Distinct()
            .GroupBy(_ => 1)
            .Select(g => new Statistic
            {
                Wins = g.Count(x => x.IsWin),
                Defeats = g.Count(x => !x.IsWin),
            })
            .FirstOrDefaultAsync();

        return statistic ?? Statistic.Default;
    }

    public async Task<IEnumerable<StatisticWithFilterDto>> GetStatisticsFromUserFilters(IUserIdentity user)
    {
        var statistics = await _mapper.ProjectTo<StatisticWithFilterDto>(
            _dbContext.StatisticFilters
            .Join(_dbContext.GameStatistics,
                filter => filter.UserId,
                gameStatistic => gameStatistic.UserId,
                (filter, gameStatistic) => new GameFilter
                {
                    Filter = filter,
                    Game = gameStatistic
                })
            .Where(x => x.Filter.UserId == user.Id)
            .Where(StatisticFilterBase.GameMather)
            .Select(x => new
            {
                x.Filter,
                x.Game.GameDetailId,
                x.Game.IsWin,
            })
            .Distinct()
            .GroupBy(x => x.Filter, x => x)
            .Select(g => new StatisticWithFilter
            {
                Filter = g.Key,
                Statistic = new Statistic
                {
                    Wins = g.Count(x => x.IsWin),
                    Defeats = g.Count(x => !x.IsWin)
                }
            }))
            .OrderBy(x => x.Filter.CreatedAt)
            .ToListAsync();

        return statistics;
    }

    public async Task<StatisticWithFilterDto> AddFilter(StatisticFilterCreateDto filter, IUserIdentity actor)
    {
        if (!await _dbContext.Users.AnyAsync(x => x.Id == actor.Id))
        {
            throw new EntityNotFoundException<User, string>(actor.Id);
        }

        var newFilter = _mapper.Map<StatisticFilter>(filter);

        newFilter.UserId = actor.Id;
        newFilter.Id = Guid.NewGuid().ToString();
        newFilter.CreatedAt = _timeProvider.GetTime();

        _dbContext.StatisticFilters.Add(newFilter);
        await _dbContext.SaveChangesAsync();

        var statistic = await _dbContext.GameStatistics
            .Where(x => x.UserId == actor.Id)
            .ApplyFilter(filter)
            .Select(x => new { x.GameDetailId, x.IsWin })
            .Distinct()
            .GroupBy(_ => 1)
            .Select(g => new Statistic
            {
                Wins = g.Count(x => x.IsWin),
                Defeats = g.Count(x => !x.IsWin),
            })
            .FirstOrDefaultAsync();

        statistic ??= Statistic.Default;

        return new StatisticWithFilterDto
        {
            Statistic = statistic,
            Filter = _mapper.Map<StatisticFilterPublicDto>(newFilter)
        };
    }

    public async Task DeleteFilter(string filterId, IUserIdentity actor)
    {
        var filter = await _dbContext.StatisticFilters
            .FirstOrDefaultAsync(x => x.Id == filterId);
        if (filter is null)
        {
            throw new EntityNotFoundException<StatisticFilter, string>(filterId);
        }

        if (filter.UserId != actor.Id)
        {
            throw new AccessForbiddenException();
        }

        _dbContext.StatisticFilters.Remove(filter);
        await _dbContext.SaveChangesAsync();
    }
}
