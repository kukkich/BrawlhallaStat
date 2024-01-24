using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Api.Replays.Cache;
using BrawlhallaStat.Api.Replays.ReplayHandling;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.Replays;

public static class ServicesExtension
{
    public static void AddReplay(this IServiceCollection services)
    {
        services.AddScoped<ICacheService<Weapon>, WeaponCacheService>();
        services.AddScoped<ICacheService<Legend>, LegendCacheService>();

        services.AddReplayHandlingPipeline();
    }
}