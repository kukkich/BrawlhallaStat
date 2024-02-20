namespace BrawlhallaStat.Api.Caching;

public interface ICacheService<T>
{
    Task<T> GetDataAsync();
}