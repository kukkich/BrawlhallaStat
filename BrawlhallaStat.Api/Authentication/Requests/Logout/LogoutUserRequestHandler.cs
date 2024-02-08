using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Logout;

public class LogoutUserRequestHandler : IRequestHandler<LogoutUserRequest>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public LogoutUserRequestHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserRequest> logger
    )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(LogoutUserRequest request, CancellationToken cancellationToken)
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