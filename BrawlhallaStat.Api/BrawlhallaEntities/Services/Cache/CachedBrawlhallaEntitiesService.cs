using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services.Cache;

public class CachedBrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly IBrawlhallaEntitiesService _originalService;
    private readonly ICacheService<List<Legend>> _legendsCache;
    private readonly ICacheService<List<Weapon>> _weaponCache;

    public CachedBrawlhallaEntitiesService(
        IBrawlhallaEntitiesService originalService,
        ICacheService<List<Legend>> legendsCache,
        ICacheService<List<Weapon>> weaponCache
    )
    {
        _originalService = originalService;
        _legendsCache = legendsCache;
        _weaponCache = weaponCache;
    }

    public async Task<List<Legend>> GetLegends()
    {
        return await _legendsCache.GetOrCreateAsync(_originalService.GetLegends);
    }

    public async Task<List<Weapon>> GetWeapons()
    {
        return await _weaponCache.GetOrCreateAsync(_originalService.GetWeapons);
    }

    public Task AddWeapon(WeaponDto weapon)
    {
        return _originalService.AddWeapon(weapon);
    }

    public Task AddLegend(AddLegendDto legend)
    {
        return _originalService.AddLegend(legend);
    }
}