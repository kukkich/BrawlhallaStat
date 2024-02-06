using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BrawlhallaStat.Api.Services.Tokens;

//TODO replace to configuration
internal static class TokenConfig
{
    public const string Issuer = "BHStat.Api";
    public const string Audience = "BHStat.Client";
    public static TimeSpan AccessLifeTime => TimeSpan.FromSeconds(15);
    public static TimeSpan RefreshLifeTime => TimeSpan.FromDays(30);

    private const string AccessKey = "24e6b622-579e-44dd-a5de-3836da322ad5";
    private const string RefreshKey = "24e6b622-579e-44dd-a5de-5436da322ad5";

    public static SymmetricSecurityKey GetSymmetricSecurityAccessKey() => new(Encoding.UTF8.GetBytes(AccessKey));
    public static SymmetricSecurityKey GetSymmetricSecurityRefreshKey() => new(Encoding.UTF8.GetBytes(RefreshKey));
}