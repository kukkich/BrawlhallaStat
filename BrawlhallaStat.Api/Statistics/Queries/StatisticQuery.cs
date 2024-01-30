using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record StatisticQuery(IUserIdentity User, StatisticGeneralFilter Filter) 
    : IRequest<Statistic>;
