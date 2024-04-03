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

    private readonly SemaphoreSlim _dataLoadSemaphoreSlim = new (1, 1);

    protected MemCacheBase(IMemoryCache cache, BrawlhallaStatContext dbContext)
    {
        _cache = cache;
        DbContext = dbContext;
    }

    public async Task<T> GetDataAsync()
    {
        var dataLoaded = _cache.TryGetValue(CacheKey, out T data);
        if (dataLoaded) return data!;

        await _dataLoadSemaphoreSlim.WaitAsync();

        try
        {
            if (!_cache.TryGetValue(CacheKey, out data))
            {
                data = await LoadDataAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(Expiration);

                _cache.Set(CacheKey, data, cacheOptions);
            }
        }
        finally
        {
            _dataLoadSemaphoreSlim.Release();
        }

        return data!;
    }

    protected abstract Task<T> LoadDataAsync();
}