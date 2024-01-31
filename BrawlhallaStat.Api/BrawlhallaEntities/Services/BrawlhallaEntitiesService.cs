using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public class BrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly ICacheService<List<Legend>> _legendsCache;
    private readonly ICacheService<List<Weapon>> _weaponsCache;

    public BrawlhallaEntitiesService(
        ICacheService<List<Legend>> legendsCache,
        ICacheService<List<Weapon>> weaponsCache
    )
    {
        _legendsCache = legendsCache;
        _weaponsCache = weaponsCache;
    }

    public async Task<List<Legend>> GetLegends()
    {
        var legends = await _legendsCache.GetDataAsync();
        return legends;
    }

    public async Task<List<Weapon>> GetWeapons()
    {
        var weapons = await _weaponsCache.GetDataAsync();
        return weapons;
    }
}