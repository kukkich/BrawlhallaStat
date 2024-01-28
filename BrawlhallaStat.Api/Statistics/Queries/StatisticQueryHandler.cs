using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class StatisticQueryHandler : IRequestHandler<StatisticQuery, Statistic>
{
    private readonly IStatisticService _statisticService;

    public StatisticQueryHandler(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public Task<Statistic> Handle(StatisticQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}