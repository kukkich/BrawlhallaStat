using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Api.Authentication.Services.Hashing;
using BrawlhallaStat.Api.Authentication.Services.Tokens;

namespace BrawlhallaStat.Api.Authentication;

public static class ServicesExtension
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IPasswordHasher, IdentityHasher>();
        services.AddTokenService();
    }
}