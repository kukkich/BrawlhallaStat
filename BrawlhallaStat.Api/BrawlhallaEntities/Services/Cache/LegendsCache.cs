using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.GameEntities;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services.Cache;

public class LegendsCache : MemCacheBase<List<Legend>>
{
    protected override string CacheKey => "LegendCache";

    public LegendsCache(IMemoryCache cache)
        : base(cache)
    { }
}