using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record UserStatisticFiltersQuery(IUserIdentity User)
    : IRequest<IEnumerable<Statistic>>;