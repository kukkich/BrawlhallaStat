using BrawlhallaStat.Api.Contracts.Statistics;
using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Requests;

public record AddFilterRequest(IUserIdentity Actor, StatisticFilterCreateDto Filter) 
    : IRequest<StatisticWithFilterDto>;
