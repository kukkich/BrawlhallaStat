using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Cache;

public class WeaponsCache : MemCacheBase<List<Weapon>>
{
    protected override string CacheKey => "WeaponCache";

    public WeaponsCache(IMemoryCache cache, BrawlhallaStatContext dbContext)
        : base(cache, dbContext)
    { }

    protected override async Task<List<Weapon>> LoadDataAsync()
    {
        return await DbContext.Weapons
            .AsNoTracking()
            .ToListAsync();
    }
}