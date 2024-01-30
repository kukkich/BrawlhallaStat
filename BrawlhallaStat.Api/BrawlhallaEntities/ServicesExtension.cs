using BrawlhallaStat.Api.BrawlhallaEntities.Cache;
using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.BrawlhallaEntities;

public static class ServicesExtension
{
    public static void AddBrawlhallaEntities(this IServiceCollection services)
    {
        services.AddScoped<ICacheService<List<Weapon>>, WeaponsCache>();
        services.AddScoped<ICacheService<List<Legend>>, LegendsCache>();
        services.AddScoped<IBrawlhallaEntitiesService, BrawlhallaEntitiesService>();
    }
}