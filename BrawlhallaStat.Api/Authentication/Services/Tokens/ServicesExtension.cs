namespace BrawlhallaStat.Api.Authentication.Services.Tokens;

public static class ServicesExtension
{
    public static void AddTokenService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}