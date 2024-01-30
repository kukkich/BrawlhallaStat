using BrawlhallaStat.Api.BrawlhallaData.Cache;
using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.BrawlhallaData;

public static class ServicesExtension
{
    public static void AddBrawlhallaDataCache(this IServiceCollection services)
    {
        services.AddScoped<ICacheService<List<Weapon>>, WeaponCacheService>();
        services.AddScoped<ICacheService<List<Legend>>, LegendCacheService>();
    }
}