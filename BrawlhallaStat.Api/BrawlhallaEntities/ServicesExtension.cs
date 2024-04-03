using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Api.BrawlhallaEntities.Services.Cache;
using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain.GameEntities;

namespace BrawlhallaStat.Api.BrawlhallaEntities;

public static class ServicesExtension
{
    public static void AddBrawlhallaEntities(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService<List<Weapon>>, WeaponsCache>();
        services.AddSingleton<ICacheService<List<Legend>>, LegendsCache>();
        services.AddScoped<BrawlhallaEntitiesService>();
        services.AddScoped<IBrawlhallaEntitiesService, CachedBrawlhallaEntitiesService>(
            x => new CachedBrawlhallaEntitiesService(
                x.GetRequiredService<BrawlhallaEntitiesService>(),
                x.GetRequiredService<ICacheService<List<Legend>>>(),
                x.GetRequiredService<ICacheService<List<Weapon>>>()
            )
        );
    }
}