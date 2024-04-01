using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class StatisticByFilterQueryHandler : IRequestHandler<StatisticByFilterQuery, Statistic>
{
    private readonly IStatisticService _statisticService;

    public StatisticByFilterQueryHandler(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public async Task<Statistic> Handle(StatisticByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = await _statisticService.GetStatistic(request.Filter, request.User);

        return result;
    }
}