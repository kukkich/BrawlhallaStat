using BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling;

public static class ServicesExtension
{
    public static void AddReplayHandlingPipeline(this IServiceCollection services)
    {
        services.AddScoped<ReplayHandlingPipeline>();


        services.AddScoped<IReplayHandler, UserLoaderHandler>();
        services.AddScoped<IReplayHandler, UserExistValidationHandler>();
        services.AddScoped<IReplayHandler, LoadDataFromDbHandler>();
       
        services.AddScoped<IReplayHandler, SelectWinLoseCounterHandler>();
        
        services.AddScoped<IReplayHandler, TotalStatisticHandler>();
        
        services.AddScoped<IReplayHandler, LegendStatisticsHandler>();
        services.AddScoped<IReplayHandler, LegendAgainstLegendStatisticsHandler>();
        services.AddScoped<IReplayHandler, LegendAgainstWeaponStatisticsHandler>();

        services.AddScoped<IReplayHandler, WeaponStatisticsHandler>();
        services.AddScoped<IReplayHandler, WeaponAgainstLegendStatisticsHandler>();
        services.AddScoped<IReplayHandler, WeaponAgainstWeaponStatisticsHandler>();
        
    }
}