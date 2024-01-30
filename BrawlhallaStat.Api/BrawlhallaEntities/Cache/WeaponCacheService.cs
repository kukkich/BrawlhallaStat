using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.BrawlhallaData.Cache;

public class WeaponCacheService : MemCacheBase<List<Weapon>>
{
    protected override string CacheKey => "WeaponCache";

    public WeaponCacheService(IMemoryCache cache, BrawlhallaStatContext dbContext)
        : base(cache, dbContext)
    { }

    protected override async Task<List<Weapon>> LoadDataAsync()
    {
        return await DbContext.Weapons.AsNoTracking().ToListAsync();
    }
}