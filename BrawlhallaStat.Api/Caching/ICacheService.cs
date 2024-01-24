namespace BrawlhallaStat.Api.Caching;

public interface ICacheService<T>
{
    // Todo replace with Task<T> return value
    Task<List<T>> GetDataAsync();
}