namespace BrawlhallaStat.Api.General.Time;

public static class ServicesExtensions
{
    public static void AddTimeProvider(this IServiceCollection services)
    {
        services.AddTransient<ITimeProvider, CurrentTimeProvider>();
    }
}