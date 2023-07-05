using BrawlhallaStat.Api.Services.Cache;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Statistics;
// ReSharper disable PossibleMultipleEnumeration

namespace BrawlhallaStat.Api.Factories;

public class StatisticFactory : IStatisticFactory
{
    private readonly ICacheService<Legend> _legends;
    private readonly ICacheService<Weapon> _weapons;

    public StatisticFactory(ICacheService<Legend> legends, ICacheService<Weapon> weapons)
    {
        _legends = legends;
        _weapons = weapons;
    }

    public async Task<WeaponAgainstWeaponStatistic[]> CreateWeaponAgainstWeapon(string userId)
    {
        return CreatePair(
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            (entityId, opponentEntityId) => new WeaponAgainstWeaponStatistic
            {
                WeaponId = entityId,
                OpponentWeaponId = opponentEntityId
            }, userId
        );
    }

    public async Task<WeaponAgainstLegendStatistic[]> CreateWeaponAgainstLegend(string userId)
    {
        return CreatePair(
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            (await _legends.GetDataAsync()).Select(x => x.Id),
            (entityId, opponentEntityId) => new WeaponAgainstLegendStatistic
            {
                WeaponId = entityId,
                OpponentLegendId = opponentEntityId
            },
            userId
        );
    }

    public async Task<LegendAgainstWeaponStatistic[]> CreateLegendAgainstWeapon(string userId)
    {
        return CreatePair(
            (await _legends.GetDataAsync()).Select(x => x.Id),
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            (entityId, opponentEntityId) => new LegendAgainstWeaponStatistic
            {
                LegendId = entityId,
                OpponentWeaponId = opponentEntityId
            },
            userId
        );
    }

    public async Task<LegendAgainstLegendStatistic[]> CreateLegendAgainstLegend(string userId)
    {
        return CreatePair(
            (await _legends.GetDataAsync()).Select(x => x.Id),
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            (entityId, opponentEntityId) => new LegendAgainstLegendStatistic
            {
                LegendId = entityId,
                OpponentLegendId = opponentEntityId
            },
            userId
        );
    }

    public async Task<WeaponStatistic[]> CreateWeapon(string userId)
    {
        return Create(
            (await _weapons.GetDataAsync()).Select(x => x.Id),
            entityId => new WeaponStatistic { WeaponId = entityId },
            userId
        );
    }

    public async Task<LegendStatistic[]> CreateLegend(string userId)
    {
        return Create(
            (await _legends.GetDataAsync()).Select(x => x.Id),
            entityId => new LegendStatistic { LegendId = entityId },
            userId
        );
    }

    public Statistic CreateSimple()
    {
        return new Statistic { Id = Guid.NewGuid().ToString() };
    }

    private TStatistic[] CreatePair<TStatistic>(
        IEnumerable<int> objectIds,
        IEnumerable<int> opponentObjectIds,
        Func<int, int, TStatistic> statisticFactory,
        string userId)
        where TStatistic : IConcreteStatistic
    {
        var statistics = new TStatistic[objectIds.Count() * opponentObjectIds.Count()];

        var i = 0;
        foreach (var objectId in objectIds)
        {
            foreach (var opponentObjectId in opponentObjectIds)
            {
                statistics[i] = statisticFactory(objectId, opponentObjectId);
                statistics[i].Statistic = CreateSimple();
                statistics[i].UserId = userId;
                statistics[i].Id = Guid.NewGuid().ToString();
                i++;
            }
        }

        return statistics.ToArray();
    }

    private TStatistic[] Create<TStatistic>(
        IEnumerable<int> objectIds,
        Func<int, TStatistic> statisticFactory,
        string userId)
        where TStatistic : IConcreteStatistic
    {
        var statistics = new TStatistic[objectIds.Count()];

        var i = 0;
        foreach (var objectId in objectIds)
        {
            statistics[i] = statisticFactory(objectId);
            statistics[i].Statistic = CreateSimple();
            statistics[i].UserId = userId;
            statistics[i].Id = Guid.NewGuid().ToString();
            i++;
        }

        return statistics.ToArray();
    }
}