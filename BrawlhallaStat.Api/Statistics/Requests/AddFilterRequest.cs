using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Requests;

public record AddFilterRequest(IUserIdentity Actor, StatisticFilterCreateDto Filter) 
    : IRequest<StatisticWithFilterDto>;
