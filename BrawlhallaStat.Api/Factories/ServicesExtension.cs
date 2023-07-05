namespace BrawlhallaStat.Api.Factories;

public static class ServicesExtension
{
    public static void AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IStatisticFactory, StatisticFactory>();
    }
}