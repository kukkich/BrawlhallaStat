using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public interface IAuthenticationService
{
    public Task<TokenPair> Login(string login, string password);
    public Task<TokenPair> Logout(string refreshToken);
    public Task<TokenPair> RefreshTokens(string refreshToken);
    public Task<TokenPair> Register(RegistrationData data);
}