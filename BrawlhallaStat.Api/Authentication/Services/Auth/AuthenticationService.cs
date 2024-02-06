using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    public Task<TokenPair> Login(string login, string password)
    {
        throw new NotImplementedException();
    }

    public Task<TokenPair> Logout(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<TokenPair> RefreshTokens(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<TokenPair> Register(RegistrationData data)
    {
        throw new NotImplementedException();
    }
}