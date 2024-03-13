using System.Linq.Expressions;
using AutoMapper;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities.Views;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
            .FirstAsync();

        return statistic;
    }

    public async Task<IEnumerable<StatisticWithFilterDto>> GetStatisticsFromUserFilters(IUserIdentity user)
    {
        Expression<Func<GameFilter, bool>> gameMather = gameFilter =>
            (gameFilter.Filter.GameType == null || gameFilter.Filter.GameType == gameFilter.Game.GameType) &&
            (gameFilter.Filter.LegendId == null || gameFilter.Filter.LegendId == gameFilter.Game.LegendId) &&
            (gameFilter.Filter.WeaponId == null || gameFilter.Filter.WeaponId == gameFilter.Game.WeaponId) &&
            (gameFilter.Filter.EnemyLegendId == null || gameFilter.Filter.EnemyLegendId == gameFilter.Game.EnemyLegendId) &&
            (gameFilter.Filter.EnemyWeaponId == null || gameFilter.Filter.EnemyWeaponId == gameFilter.Game.EnemyWeaponId) &&
            (gameFilter.Filter.TeammateLegendId == null || gameFilter.Filter.TeammateLegendId == gameFilter.Game.TeammateLegendId) &&
            (gameFilter.Filter.TeammateWeaponId == null || gameFilter.Filter.TeammateWeaponId == gameFilter.Game.TeammateWeaponId);

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
            .Where(gameMather)
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

        _dbContext.StatisticFilters.Add(newFilter);
        await _dbContext.SaveChangesAsync();

        var filteredGames = _dbContext.GameStatistics
            .ApplyFilter(newFilter)
            .Select(x => new { x.GameDetailId, x.IsWin })
            .Distinct();
        var statistic = new Statistic
        {
            Wins = await filteredGames.Where(x => x.IsWin)
                .CountAsync(),
            Defeats = await filteredGames.Where(x => !x.IsWin)
                .CountAsync()
        };

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

file class GameFilter
{
    public StatisticFilter Filter { get; set; } = null!;
    public GameStatisticView Game { get; set; } = null!;
}