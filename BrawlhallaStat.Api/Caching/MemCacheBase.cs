using Microsoft.Extensions.Caching.Memory;
#pragma warning disable CS8600

namespace BrawlhallaStat.Api.Caching;

public abstract class MemCacheBase<T> : ICacheService<T>
{
    private readonly IMemoryCache _cache;
    protected abstract string CacheKey { get; }
    protected virtual TimeSpan Expiration => TimeSpan.FromMinutes(20);

    private readonly SemaphoreSlim _dataLoadSemaphore = new (1, 1);

    protected MemCacheBase(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateAsync(Func<Task<T>> factory)
    {
        var dataLoaded = _cache.TryGetValue(CacheKey, out T data);

        if (dataLoaded) return data!;

        await _dataLoadSemaphore.WaitAsync();

        try
        {
            if (!_cache.TryGetValue(CacheKey, out data))
            {
                data = await factory();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(Expiration);

                _cache.Set(CacheKey, data, cacheOptions);
            }
        }
        finally
        {
            _dataLoadSemaphore.Release();
        }

        return data!;
    }
}