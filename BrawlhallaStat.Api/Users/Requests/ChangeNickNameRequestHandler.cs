using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Users.Services;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Users.Requests;

public class ChangeNickNameRequestHandler : IRequestHandler<ChangeNickNameRequest>
{
    private readonly IUserService _userService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public ChangeNickNameRequestHandler(
        IUserService userService,
        BrawlhallaStatContext dbContext,
        ILogger<LoginUserRequest> logger
        )
    {
        _userService = userService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(ChangeNickNameRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "User {Id} change nickname transaction begin",
                request.User.Id
            );

            await _userService.ChangeNickName(request.User, request.NewNickName);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "User {Id} change nickname transaction commit",
                request.User.Id
            );
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "User {Id} change nickname transaction rollback: {Message}",
                request.User.Id, exception.Message
            );
            throw;
        }
    }
}