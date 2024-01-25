using BrawlhallaStat.Api.Replays.ReplayHandling.Handlers;

namespace BrawlhallaStat.Api.Replays.ReplayHandling;

public static class ServicesExtension
{
    public static void AddReplayHandlingPipeline(this IServiceCollection services)
    {
        services.AddScoped<IReplayHandler, LoggingHandler>();
    }
}