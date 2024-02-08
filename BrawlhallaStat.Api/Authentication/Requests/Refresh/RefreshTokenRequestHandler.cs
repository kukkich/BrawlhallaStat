using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Authentication.Services.Auth;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Refresh;

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, TokenPair>
{
    private readonly IAuthenticationService _authService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public RefreshTokenRequestHandler(
        IAuthenticationService authService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserRequest> logger
    )
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TokenPair> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Token {token} refresh transaction begin",
                request.RefreshToken
            );

            var tokenPair = await _authService.RefreshTokens(request.RefreshToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "Token {token} refresh transaction commit",
                request.RefreshToken
            );

            return tokenPair;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "Token {token} refresh transaction rollback",
                request.RefreshToken
            );
            throw;
        }
    }
}