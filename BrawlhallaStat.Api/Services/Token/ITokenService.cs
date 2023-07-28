using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Services.Token;

public interface ITokenService
{
    TokenPair GenerateTokenPair(User user);
    TokenPair RefreshAccessToken(string refreshToken);
    void RevokeRefreshToken(string refreshToken);
}