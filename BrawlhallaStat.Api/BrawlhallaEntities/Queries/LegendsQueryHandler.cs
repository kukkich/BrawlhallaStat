using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class LegendsQueryHandler : IRequestHandler<LegendsQuery, List<Legend>>
{
    private readonly IBrawlhallaEntitiesService _entitiesService;

    public LegendsQueryHandler(IBrawlhallaEntitiesService entitiesService)
    {
        _entitiesService = entitiesService;
    }

    public async Task<List<Legend>> Handle(LegendsQuery request, CancellationToken cancellationToken)
    {
        var legends = await _entitiesService.GetLegends();
        return legends;
    }
}