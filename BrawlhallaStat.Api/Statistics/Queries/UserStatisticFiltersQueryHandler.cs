using BrawlhallaStat.Api.Statistics.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public class UserStatisticFiltersQueryHandler 
    : IRequestHandler<UserStatisticFiltersQuery, IEnumerable<Statistic>>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IStatisticService _statisticService;

    public UserStatisticFiltersQueryHandler(BrawlhallaStatContext dbContext, IStatisticService statisticService)
    {
        _dbContext = dbContext;
        _statisticService = statisticService;
    }

    public Task<IEnumerable<Statistic>> Handle(UserStatisticFiltersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}