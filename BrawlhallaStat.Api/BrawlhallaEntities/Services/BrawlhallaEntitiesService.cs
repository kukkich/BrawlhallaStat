using AutoMapper;
using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public class BrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly ICacheService<List<Legend>> _legendsCache;
    private readonly ICacheService<List<Weapon>> _weaponsCache;
    private readonly BrawlhallaStatContext _dbContext;

    public BrawlhallaEntitiesService(
        ICacheService<List<Legend>> legendsCache,
        ICacheService<List<Weapon>> weaponsCache, 
        BrawlhallaStatContext dbContext, 
        IMapper mapper
    )
    {
        _legendsCache = legendsCache;
        _weaponsCache = weaponsCache;
        _dbContext = dbContext;
    }

    public async Task<List<Legend>> GetLegends()
    {
        var legends = await _legendsCache.GetDataAsync();
        return legends;
    }

    public Task<Legend> AddLegend()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Weapon>> GetWeapons()
    {
        var weapons = await _weaponsCache.GetDataAsync();
        return weapons;
    }

    public Task<Legend> AddWeapon()
    {
        throw new NotImplementedException();
    }
}