using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class WeaponsQueryHandler : IRequestHandler<WeaponsQuery, List<Weapon>>
{
    private readonly IBrawlhallaEntitiesService _entitiesService;

    public WeaponsQueryHandler(IBrawlhallaEntitiesService entitiesService)
    {
        _entitiesService = entitiesService;
    }

    public async Task<List<Weapon>> Handle(WeaponsQuery request, CancellationToken cancellationToken)
    {
        return await _entitiesService.GetWeapons();
    }
}