using BrawlhallaStat.Api.Authentication.Services.Hashing;
using BrawlhallaStat.Api.Authentication.Services.Tokens;
using BrawlhallaStat.Api.Exceptions.Authentication;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticationService(
        ITokenService tokenService,
        BrawlhallaStatContext dbContext,
        ILogger<AuthenticationService> logger,
        IPasswordHasher passwordHasher
    )
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<TokenPair> Login(string login, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Login == login);
        if (user == null)
        {
            throw new UserNotExistException(login);
        }

        if (!_passwordHasher.Compare(user.PasswordHash, password))
        {
            throw new InvalidPasswordException();
        }

        var newTokenPair = await _tokenService.GenerateTokenPair(user);

        _logger.LogInformation(
            "User logged in: id {Id}, login {Login}",
            user.Id, user.Login
        );
        return newTokenPair;
    }

    public async Task Logout(string refreshToken)
    {
        await _tokenService.RevokeRefreshToken(refreshToken);
    }

    public async Task<TokenPair> RefreshTokens(string refreshToken)
    {
        var tokenPair = await _tokenService.RefreshAccessToken(refreshToken);

        return tokenPair;
    }

    public Task<TokenPair> Register(RegistrationData data)
    {
        throw new NotImplementedException();
    }
}