namespace BrawlhallaStat.Api.Services.Cache;

public interface ICacheService<T>
{
    Task<List<T>> GetDataAsync();
}