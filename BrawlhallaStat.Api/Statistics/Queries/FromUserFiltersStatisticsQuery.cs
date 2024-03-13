using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record FromUserFiltersStatisticsQuery(IUserIdentity User)
    : IRequest<IEnumerable<StatisticWithFilterDto>>;
