using BrawlhallaStat.Api.Authentication.Commands.Login;
using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Api.Services.Tokens;
using BrawlhallaStat.Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Authentication.Commands.Logout;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserCommand> _logger;

    public LogoutUserCommandHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserCommand> logger
    )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation("User logout transaction begin");

            await _authService.Logout(request.RefreshToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("User logout transaction commit");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning("User logout transaction rollback");
            throw;
        }
    }
}