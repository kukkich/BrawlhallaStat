using BrawlhallaStat.Domain.Context;
using Microsoft.Extensions.Caching.Memory;
#pragma warning disable CS8600

namespace BrawlhallaStat.Api.Services.Cache;

public abstract class MemCacheBase<T> : ICacheService<T>
{
    private readonly IMemoryCache _cache;
    protected readonly BrawlhallaStatContext DbContext;
    protected abstract string CacheKey { get; }

    protected MemCacheBase(IMemoryCache cache, BrawlhallaStatContext dbContext)
    {
        _cache = cache;
        DbContext = dbContext;
    }

    public async Task<List<T>> GetDataAsync()
    {
        if (_cache.TryGetValue(CacheKey, out List<T> data))
            return data!;

        data = await LoadDataFromDatabaseAsync();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

        _cache.Set(CacheKey, data, cacheOptions);

        return data;
    }

    protected abstract Task<List<T>> LoadDataFromDatabaseAsync();
}