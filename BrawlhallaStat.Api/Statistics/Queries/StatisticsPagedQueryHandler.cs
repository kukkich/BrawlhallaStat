using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class StatisticsPagedQueryHandler
    : IRequestHandler<StatisticsPagedQuery, PagedStatisticWithFilterDto>
{
    private readonly IStatisticService _statisticService;
    private readonly ILogger<LoginUserRequest> _logger;

    public StatisticsPagedQueryHandler(IStatisticService statisticService, ILogger<LoginUserRequest> logger)
    {
        _statisticService = statisticService;
        _logger = logger;
    }

    public async Task<PagedStatisticWithFilterDto> Handle(StatisticsPagedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation(
                "Query statistics user {UserId}",
                request.User.Id
            );

            var result = await _statisticService.GetStatisticsFromUserFilters(request.User, request.Page);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                "Query statistics user {UserId} error: {Message}",
                request.User.Id, exception.Message
            );
            throw;
        }
    }
}