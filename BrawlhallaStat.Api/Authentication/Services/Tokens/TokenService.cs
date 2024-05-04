using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BrawlhallaStat.Api.Authentication.Exceptions.Tokens;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BrawlhallaStat.Api.Authentication.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<TokenService> _logger;
    private readonly TokenConfig _tokenConfig;

    public TokenService(
        BrawlhallaStatContext dbContext,
        IMapper mapper,
        ILogger<TokenService> logger,
        TokenConfig tokenConfig
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _tokenConfig = tokenConfig;
    }

    public async Task<TokenPair> GenerateTokenPair(IUserIdentity user)
    {
        var userClaims = _mapper.Map<List<Claim>>(user);

        var accessJwt = CreateAccessToken(userClaims);
        var refreshJwt = CreateRefreshToken(userClaims);
        var tokenPair = WriteTokens(accessJwt, refreshJwt);

        await SaveToken(user.Id, tokenPair.Refresh, refreshJwt);

        return tokenPair;
    }

    private static TokenPair WriteTokens(SecurityToken accessJwt, SecurityToken refreshJwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenPair
        {
            Access = tokenHandler.WriteToken(accessJwt),
            Refresh = tokenHandler.WriteToken(refreshJwt)
        };
    }

    private JwtSecurityToken CreateAccessToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: _tokenConfig.Issuer,
            audience: _tokenConfig.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(_tokenConfig.AccessLifeTime),
            signingCredentials: new SigningCredentials(
                _tokenConfig.GetSymmetricSecurityAccessKey(),
                SecurityAlgorithms.HmacSha256
            )
        );

        return jwt;
    }

    private JwtSecurityToken CreateRefreshToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: _tokenConfig.Issuer,
            audience: _tokenConfig.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(_tokenConfig.RefreshLifeTime),
            signingCredentials: new SigningCredentials(
                _tokenConfig.GetSymmetricSecurityRefreshKey(),
                SecurityAlgorithms.HmacSha256
            )
        );

        return jwt;
    }

    private async Task SaveToken(string userId, string value, SecurityToken jwt)
    {
        var token = new Token
        {
            Id = Guid.NewGuid().ToString(),
            RefreshToken = value,
            UserId = userId,
            ExpiresAt = jwt.ValidTo,
            ValidFrom = jwt.ValidFrom,
        };
        _dbContext.Tokens.Add(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<LoginResult> RefreshAccessToken(string refreshToken)
    {
        var isTokenValid = await IsRefreshTokenValid(refreshToken);
        if (!isTokenValid)
        {
            throw new InvalidRefreshTokenException();
        }

        var now = DateTime.Now;
        var tokenFromStorage = await _dbContext.Tokens
            .Include(x => x.User)
            .Where(x => x.ExpiresAt > now)
            .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);

        if (tokenFromStorage is null)
        {
            throw new TokenNotFoundException();
        }

        var user = tokenFromStorage.User;

        var tokenPair = await GenerateTokenPair(user);

        _dbContext.Tokens.Remove(tokenFromStorage);
        await _dbContext.SaveChangesAsync();

        var authenticatedUser = _mapper.Map<AuthenticatedUser>(user);

        return new LoginResult
        {
            TokenPair = tokenPair,
            User = authenticatedUser
        };
    }

    private async Task<bool> IsRefreshTokenValid(string token)
    {
        var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(
            token,
            new TokenValidationParameters
            {
                ValidIssuer = _tokenConfig.Issuer,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = _tokenConfig.Audience,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                IssuerSigningKey = _tokenConfig.GetSymmetricSecurityRefreshKey(),
            }
        );
        return result.IsValid;
    }

    public async Task RevokeRefreshToken(string refreshToken)
    {
        var token = await _dbContext.Tokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);

        if (token is null)
        {
            _logger.LogWarning("Token {refreshToken} wasn't found during revocation", refreshToken);
            throw new TokenNotFoundException();
        }

        _dbContext.Tokens.Remove(token);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation(
            "User logged out: id {Id}, login {Login}",
            token.User.Id, token.User.Login
        );
    }
}