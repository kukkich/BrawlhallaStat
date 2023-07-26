using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.Services.Cache;

public class LegendCacheService : MemCacheBase<Legend>
{
    protected override string CacheKey => "LegendCache";

    public LegendCacheService(IMemoryCache cache, BrawlhallaStatContext dbContext)
        : base(cache, dbContext)
    { }

    protected override async Task<List<Legend>> LoadDataFromDatabaseAsync()
    {
        return await DbContext.Legends
            .Include(x => x.FirstWeapon)
            .Include(x => x.SecondWeapon)
            .AsNoTracking().ToListAsync();
    }
}