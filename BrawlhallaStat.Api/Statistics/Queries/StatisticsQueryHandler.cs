﻿using BrawlhallaStat.Api.Authentication.Requests.Login;
using BrawlhallaStat.Api.Contracts.Statistics;
using BrawlhallaStat.Api.Statistics.Services;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class StatisticsQueryHandler: IRequestHandler<StatisticsQuery, IEnumerable<StatisticWithFilterDto>>
{
    private readonly IStatisticService _statisticService;
    private readonly ILogger<LoginUserRequest> _logger;

    public StatisticsQueryHandler(IStatisticService statisticService, ILogger<LoginUserRequest> logger)
    {
        _statisticService = statisticService;
        _logger = logger;
    }

    public async Task<IEnumerable<StatisticWithFilterDto>> Handle(StatisticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation(
                "Query statistics user {UserId}",
                request.User.Id
            );

            var result = await _statisticService.GetStatisticsFromUserFilters(request.User);

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