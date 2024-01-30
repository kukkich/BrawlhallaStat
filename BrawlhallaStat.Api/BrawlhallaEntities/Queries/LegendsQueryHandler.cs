using BrawlhallaStat.Domain.Games.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class LegendsQueryHandler : IRequestHandler<LegendsQuery, List<LegendDto>>
{
    public Task<List<LegendDto>> Handle(LegendsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}