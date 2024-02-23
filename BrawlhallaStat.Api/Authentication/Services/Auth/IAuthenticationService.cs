using BrawlhallaStat.Domain.Identity.Authentication;

namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public interface IAuthenticationService
{
    public Task<LoginResult> Login(string login, string password);
    public Task Logout(string refreshToken);
    public Task<LoginResult> RefreshTokens(string refreshToken);
    public Task<LoginResult> Register(RegistrationData data);
}