namespace BrawlhallaStat.Api.Caching;

public interface ICacheService<T>
{
    Task<T> GetOrCreateAsync(Func<Task<T>> factory);
}