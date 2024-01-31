using BrawlhallaStat.Domain.Context;
using Microsoft.Extensions.Caching.Memory;
#pragma warning disable CS8600

namespace BrawlhallaStat.Api.Caching;

public abstract class MemCacheBase<T> : ICacheService<T>
{
    private readonly IMemoryCache _cache;
    protected readonly BrawlhallaStatContext DbContext;
    protected abstract string CacheKey { get; }
    protected virtual TimeSpan Expiration => TimeSpan.FromHours(1);

    protected MemCacheBase(IMemoryCache cache, BrawlhallaStatContext dbContext)
    {
        _cache = cache;
        DbContext = dbContext;
    }

    public async Task<T> GetDataAsync()
    {
        if (_cache.TryGetValue(CacheKey, out T data))
            return data!;

        data = await LoadDataAsync();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(Expiration);

        _cache.Set(CacheKey, data, cacheOptions);

        return data;
    }

    protected abstract Task<T> LoadDataAsync();
}