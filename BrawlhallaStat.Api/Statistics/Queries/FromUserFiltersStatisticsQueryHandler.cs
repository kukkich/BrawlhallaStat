using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class FromUserFiltersStatisticsQueryHandler 
    : IRequestHandler<FromUserFiltersStatisticsQuery, IEnumerable<StatisticWithFilterDto>>
{
    private readonly IStatisticService _statisticService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<LoginUserRequest> _logger;

    public FromUserFiltersStatisticsQueryHandler(IStatisticService statisticService, BrawlhallaStatContext dbContext, ILogger<LoginUserRequest> logger)
    {
        _statisticService = statisticService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<StatisticWithFilterDto>> Handle(FromUserFiltersStatisticsQuery request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Query statistics from user {UserId} filters transaction begin",
                request.User.Id
            );

            var result = await _statisticService.GetStatisticsFromUserFilters(request.User);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Query statistics from user {UserId} filters transaction commit",
                request.User.Id
            );
            return result;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);

            _logger.LogWarning(
                "Query statistics from user {UserId} filters transaction rollback: {Message}",
                request.User.Id, exception.Message
            );
            throw;
        }
    }
}