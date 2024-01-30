using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class StatisticQueryHandler : IRequestHandler<StatisticQuery, Statistic>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IStatisticService _statisticService;

    public StatisticQueryHandler(BrawlhallaStatContext dbContext, IStatisticService statisticService)
    {
        _dbContext = dbContext;
        _statisticService = statisticService;
    }

    public async Task<Statistic> Handle(StatisticQuery request, CancellationToken cancellationToken)
    {
        var result = await _statisticService.GetStatisticsAsync(request.Filter, request.User);

        return result;
    }
}