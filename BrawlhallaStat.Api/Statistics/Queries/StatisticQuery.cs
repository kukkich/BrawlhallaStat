using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record StatisticQuery(IUserIdentity User, StatisticFilterCreateDto FilterCreate) 
    : IRequest<Statistic>;
