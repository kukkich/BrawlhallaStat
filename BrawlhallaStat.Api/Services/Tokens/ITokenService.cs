using System.Security.Claims;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Services.Tokens;

public interface ITokenService
{
    Task<TokenPair> GenerateTokenPair(IUserIdentity user);
    Task<TokenPair> RefreshAccessToken(string refreshToken);
    Task RevokeRefreshToken(string refreshToken);
    //Task<Token> SaveToken(string userId, string refreshToken);
}