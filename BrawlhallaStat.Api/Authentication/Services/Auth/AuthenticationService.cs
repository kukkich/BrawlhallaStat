using AutoMapper;
using BrawlhallaStat.Api.Authentication.Exceptions;
using BrawlhallaStat.Api.Authentication.Services.Hashing;
using BrawlhallaStat.Api.Authentication.Services.Tokens;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public AuthenticationService(
        ITokenService tokenService,
        BrawlhallaStatContext dbContext,
        ILogger<AuthenticationService> logger,
        IPasswordHasher passwordHasher,
        IMapper mapper
    )
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<LoginResult> Login(string login, string password)
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
        var authenticatedUser = _mapper.Map<AuthenticatedUser>(user);

        return new LoginResult
        {
            TokenPair = newTokenPair,
            User = authenticatedUser
        };
    }

    public async Task Logout(string refreshToken)
    {
        await _tokenService.RevokeRefreshToken(refreshToken);
    }

    public async Task<LoginResult> RefreshTokens(string refreshToken)
    {
        var result = await _tokenService.RefreshAccessToken(refreshToken);
        return result;
    }

    public async Task<LoginResult> Register(RegistrationData data)
    {
        var (login, nickName, password, email) = data;

        if (await _dbContext.Users.AnyAsync(x => x.Login == login))
        {
            throw new AlreadyExistException(
                who: nameof(User),
                propertyName: nameof(User.Login),
                value: login
            );
        }
        if (await _dbContext.Users.AnyAsync(x => x.Email == email))
        {
            throw new AlreadyExistException(
                who: nameof(User),
                propertyName: nameof(User.Email),
                value: email
            );
        }

        // Todo extract into user service
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Login = login,
            NickName = nickName,
            Email = email,
            PasswordHash = _passwordHasher.Hash(password)
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var newTokenPair = await _tokenService.GenerateTokenPair(user);

        _logger.LogInformation("User registered: id {Id}, login {Login}", user.Id, user.Login);

        var authenticatedUser = _mapper.Map<AuthenticatedUser>(user);

        return new LoginResult
        {
            TokenPair = newTokenPair,
            User = authenticatedUser
        };
    }
}