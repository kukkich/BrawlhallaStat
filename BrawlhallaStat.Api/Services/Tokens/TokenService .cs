using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;
using Microsoft.IdentityModel.Tokens;

namespace BrawlhallaStat.Api.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly BrawlhallaStatContext _dbContext;

    public TokenService(IConfiguration configuration, BrawlhallaStatContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public TokenPair GenerateTokenPair(IEnumerable<Claim> userClaims)
    {
        var claimsArray = userClaims as Claim[] ?? userClaims.ToArray();
        return new TokenPair
        {
            Access = CreateAccessToken(claimsArray),
            Refresh = CreateRefreshToken(claimsArray)
        };
    }

    private string CreateAccessToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: TokenConfig.Issuer,
            audience: TokenConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenConfig.LifeTime),
            signingCredentials: new SigningCredentials(
                TokenConfig.GetSymmetricSecurityAccessKey(),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    private static string CreateRefreshToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: TokenConfig.Issuer,
            audience: TokenConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TokenConfig.RefreshLifeTime),
            signingCredentials: new SigningCredentials(
                TokenConfig.GetSymmetricSecurityRefreshKey(),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public TokenPair RefreshAccessToken(string refreshToken)
    {
        // Реализация логики обновления access токена по refresh токену
        // ...

        // Вернуть новые access и refresh токены

        throw new NotImplementedException();

        return new TokenPair
        {
            Access = "new_access_token",
            Refresh = "new_refresh_token"
        };
    }

    public void RevokeRefreshToken(string refreshToken)
    {
        // Реализация логики отзыва (инвалидации) refresh токена
        // ...
        throw new NotImplementedException();
    }

    public async Task<Token> SaveToken(string userId, string refreshToken)
    {
        var token = new Token
        {
            Id = Guid.NewGuid().ToString(),
            RefreshToken = refreshToken,
            UserId = userId
        };
        _dbContext.Tokens.Add(token);
        await _dbContext.SaveChangesAsync();

        return token;
    }
}

//TODO replace to configuration
internal static class TokenConfig
{
    public const string Issuer = "BHStat.Api";
    public const string Audience = "BHStat.Client";
    public static TimeSpan LifeTime => TimeSpan.FromMinutes(15);
    public static TimeSpan RefreshLifeTime => TimeSpan.FromDays(30);

    private const string AccessKey = "24e6b622-579e-44dd-a5de-3836da322ad5";
    private const string RefreshKey = "24e6b622-579e-44dd-a5de-5436da322ad5";

    public static SymmetricSecurityKey GetSymmetricSecurityAccessKey() => new(Encoding.UTF8.GetBytes(AccessKey));
    public static SymmetricSecurityKey GetSymmetricSecurityRefreshKey() => new(Encoding.UTF8.GetBytes(RefreshKey));
}