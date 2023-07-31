using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BrawlhallaStat.Api.Exceptions.Tokens;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
#pragma warning disable IDE0270

namespace BrawlhallaStat.Api.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<TokenService> _logger;

    public TokenService(
        IConfiguration configuration, 
        BrawlhallaStatContext dbContext, 
        IMapper mapper,
        ILogger<TokenService> logger
        )
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TokenPair> GenerateTokenPair(IUserIdentity user)
    {
        var userClaims = _mapper.Map<List<Claim>>(user);
        var tokenPair = new TokenPair
        {
            Access = CreateAccessToken(userClaims),
            Refresh = CreateRefreshToken(userClaims)
        };

        await SaveToken(user.Id, tokenPair.Refresh);

        return tokenPair;
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
    private string CreateRefreshToken(IEnumerable<Claim> claims)
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
    private async Task SaveToken(string userId, string refreshToken)
    {
        var token = new Token
        {
            Id = Guid.NewGuid().ToString(),
            RefreshToken = refreshToken,
            UserId = userId
        };
        _dbContext.Tokens.Add(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TokenPair> RefreshAccessToken(string refreshToken)
    {
        var isTokenValid = IsRefreshTokenValid(refreshToken, out _);
        if (!isTokenValid)
        {
            throw new InvalidRefreshTokenException();
        }

        var tokenFromStorage = await _dbContext.Tokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        if (tokenFromStorage is null)
        {
            throw new TokenNotFoundException();
        }

        var user = tokenFromStorage.User;

        var tokenPair = await GenerateTokenPair(user);

        return tokenPair;
    }
    private bool IsRefreshTokenValid(string token, out ClaimsPrincipal? userClaimsPrincipal)
    {
        try
        {
            userClaimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidIssuer = TokenConfig.Issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = TokenConfig.Audience,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = TokenConfig.GetSymmetricSecurityRefreshKey(),
                },
                out _
            );

            return true;
        }
        catch (SecurityTokenException)
        {
            userClaimsPrincipal = null;
            return false;
        }

        _logger.LogError("Unexpected behaviour in token validation");
        throw new Exception("Unexpected behaviour in token validation");
    }

    public async Task RevokeRefreshToken(string refreshToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        
        if (token is null)
        {
            _logger.LogWarning("Token {refreshToken} wasn't found during revocation", refreshToken);
            throw new TokenNotFoundException();
        }

        _dbContext.Tokens.Remove(token);
        await _dbContext.SaveChangesAsync();
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