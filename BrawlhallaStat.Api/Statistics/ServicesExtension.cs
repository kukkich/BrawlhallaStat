using BrawlhallaStat.Api.Statistics.Services;

namespace BrawlhallaStat.Api.Statistics;

public static class ServicesExtension
{
    public static void AddStatistic(this IServiceCollection services)
    {
        services.AddScoped<IStatisticService, StatisticService>();
    }
}