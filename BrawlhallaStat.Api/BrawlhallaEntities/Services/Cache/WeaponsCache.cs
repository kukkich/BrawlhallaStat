using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.GameEntities;
using Microsoft.Extensions.Caching.Memory;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services.Cache;

public class WeaponsCache : MemCacheBase<List<Weapon>>
{
    protected override string CacheKey => "WeaponCache";

    public WeaponsCache(IMemoryCache cache)
        : base(cache)
    { }
}