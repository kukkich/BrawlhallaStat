using BrawlhallaStat.Api.Users.Services;

namespace BrawlhallaStat.Api.Users;

public static class ServiceExtension
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }
}