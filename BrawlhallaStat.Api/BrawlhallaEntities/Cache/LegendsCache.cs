using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Cache;

public class LegendsCache : MemCacheBase<List<Legend>>
{
    protected override string CacheKey => "LegendCache";

    public LegendsCache(IMemoryCache cache, BrawlhallaStatContext dbContext)
        : base(cache, dbContext)
    { }

    protected override async Task<List<Legend>> LoadDataAsync()
    {
        return await DbContext.Legends
            .Include(x => x.FirstWeapon)
            .Include(x => x.SecondWeapon)
            .AsNoTracking()
            .ToListAsync();
    }
}