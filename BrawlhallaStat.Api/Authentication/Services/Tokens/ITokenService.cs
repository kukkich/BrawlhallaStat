using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Authentication.Services.Tokens;

public interface ITokenService
{
    Task<TokenPair> GenerateTokenPair(IUserIdentity user);
    Task<LoginResult> RefreshAccessToken(string refreshToken);
    Task RevokeRefreshToken(string refreshToken);
}