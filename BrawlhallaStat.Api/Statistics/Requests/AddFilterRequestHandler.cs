using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Requests;

public class AddFilterRequestHandler : IRequestHandler<AddFilterRequest, StatisticWithFilterDto>
{
    private readonly IStatisticService _statisticService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public AddFilterRequestHandler(
        IStatisticService statisticService, 
        BrawlhallaStatContext dbContext, 
        ILogger<LoginUserRequest> logger
    )
    {
        _statisticService = statisticService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<StatisticWithFilterDto> Handle(AddFilterRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Add filter {Filter} by {UserId} transaction begin",
                request.Filter, request.Actor.Id
            );

            var result = await _statisticService.AddFilter(request.Filter, request.Actor);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Add filter {Filter} by {UserId} transaction commit",
                request.Filter, request.Actor.Id
            );
            return result;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);

            _logger.LogWarning(
                "Add filter {Filter} by {UserId} transaction rollback: {Message}",
                request.Filter, request.Actor.Id, exception.Message
            );
            throw;
        }
    }
}