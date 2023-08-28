using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Statistics.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Commands.Statistic;

public class GetLegendsStatistic : IRequest<List<LegendStatisticDto>>
{
    public AuthenticatedUser User { get; }

    public GetLegendsStatistic(AuthenticatedUser user)
    {
        User = user;
    }
}