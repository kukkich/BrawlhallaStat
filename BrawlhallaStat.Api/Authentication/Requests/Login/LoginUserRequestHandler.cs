using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Login;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, TokenPair>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public LoginUserRequestHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserRequest> logger
    )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TokenPair> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "User {Login} login transaction begin",
                request.Login
            );

            var tokens = await _authService.Login(request.Login, request.Password);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "User {Login} login transaction commit",
                request.Login
            );

            return tokens;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "User {Login} login transaction rollback: {Message}",
                request.Login, exception.Message
            );
            throw;
        }
    }
}

