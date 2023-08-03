namespace BrawlhallaStat.Api.Services.Tokens;

public static class ServicesExtension
{
    public static void AddTokenService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}