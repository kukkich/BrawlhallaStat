using System.Security.Claims;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Services.Tokens;

public interface ITokenService
{
    TokenPair GenerateTokenPair(IEnumerable<Claim> userClaims);
    TokenPair RefreshAccessToken(string refreshToken);
    void RevokeRefreshToken(string refreshToken);
    Task<Token> SaveToken(string userId, string refreshToken);
}