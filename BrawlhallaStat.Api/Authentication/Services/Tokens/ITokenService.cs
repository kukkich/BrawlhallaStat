using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Authentication.Services.Tokens;

public interface ITokenService
{
    Task<TokenPair> GenerateTokenPair(IUserIdentity user);
    Task<LoginResult> RefreshAccessToken(string refreshToken);
    Task RevokeRefreshToken(string refreshToken);
}