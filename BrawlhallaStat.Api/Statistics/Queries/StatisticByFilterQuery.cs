using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record StatisticByFilterQuery(IUserIdentity User, StatisticFilterCreateDto Filter) 
    : IRequest<Statistic>;
