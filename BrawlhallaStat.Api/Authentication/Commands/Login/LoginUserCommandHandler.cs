using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenPair>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserCommand> _logger;

    public LoginUserCommandHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserCommand> logger)
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TokenPair> Handle(LoginUserCommand request, CancellationToken cancellationToken)
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
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "User {Login} login transaction rollback",
                request.Login
            );
            throw;
        }
    }
}

