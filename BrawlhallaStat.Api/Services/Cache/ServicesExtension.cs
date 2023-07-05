using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.Services.Cache;

public static class ServicesExtension
{
    public static void AddCaching(this IServiceCollection services)
    {
        services.AddScoped<ICacheService<Weapon>, WeaponCacheService>();
        services.AddScoped<ICacheService<Legend>, LegendCacheService>();
    }
}