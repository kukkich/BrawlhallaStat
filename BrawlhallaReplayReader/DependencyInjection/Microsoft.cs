using BrawlhallaReplayReader.Deserializers;
using Microsoft.Extensions.DependencyInjection;

namespace BrawlhallaReplayReader.DependencyInjection;

public static class Microsoft
{
    public static void AddBrawlhallaReplayDeserializer(this IServiceCollection services)
    {
        services.AddTransient<IBHReplayDeserializer, BHReplayDeserializer>();
    }
}