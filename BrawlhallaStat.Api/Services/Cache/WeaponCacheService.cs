using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.Services.Cache;

public class WeaponCacheService : MemCacheBase<Weapon>
{
    protected override string CacheKey => "WeaponCache";

    public WeaponCacheService(IMemoryCache cache, BrawlhallaStatContext dbContext)
        : base(cache, dbContext)
        { }

    protected override async Task<List<Weapon>> LoadDataFromDatabaseAsync()
    {
        return await DbContext.Weapons.AsNoTracking().ToListAsync();
    }
}