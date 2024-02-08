using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BrawlhallaStat.Api.Authentication.Services.Tokens;

public class TokenConfig
{
    public string Issuer => _configuration.GetValue<string>("Issuer")!;
    public string Audience => _configuration.GetValue<string>("Audience")!;
    public TimeSpan AccessLifeTime => TimeSpan.FromSeconds(_configuration.GetValue<int>("AccessLifeTimeMinutes"));
    public TimeSpan RefreshLifeTime => TimeSpan.FromDays(_configuration.GetValue<int>("RefreshLifeTimeDays"));
    public SymmetricSecurityKey GetSymmetricSecurityAccessKey() => new(Encoding.UTF8.GetBytes(AccessKey));
    public SymmetricSecurityKey GetSymmetricSecurityRefreshKey() => new(Encoding.UTF8.GetBytes(RefreshKey));

    private string AccessKey => _configuration.GetValue<string>("AccessKey")!;
    private string RefreshKey => _configuration.GetValue<string>("RefreshKey")!;

    private readonly IConfiguration _configuration;

    public TokenConfig(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("Auth").GetSection("Token");
    }
}