using BrawlhallaStat.Api.Replays.Services;

namespace BrawlhallaStat.Api.Replays;

public static class ServicesExtension
{
    public static void AddReplay(this IServiceCollection services)
    {
        services.AddScoped<IReplayService, ReplayService>();
    }
}