using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Requests;

public class DeleteFilterRequestHandler : IRequestHandler<DeleteFilterRequest>
{
    private readonly IStatisticService _statisticService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public DeleteFilterRequestHandler(
        IStatisticService statisticService, 
        BrawlhallaStatContext dbContext, 
        ILogger<LoginUserRequest> logger
    )
    {
        _statisticService = statisticService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(DeleteFilterRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Delete filter {FilterId} by {UserId} transaction begin",
                request.FilterId, request.Actor.Id
            );

            await _statisticService.DeleteFilter(request.FilterId, request.Actor);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Delete filter {FilterId} by {UserId} transaction commit",
                request.FilterId, request.Actor.Id
            );
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);

            _logger.LogWarning(
                "Delete filter {FilterId} by {UserId} transaction rollback: {Message}",
                request.FilterId, request.Actor.Id, exception.Message
            );
            throw;
        }
    }
}