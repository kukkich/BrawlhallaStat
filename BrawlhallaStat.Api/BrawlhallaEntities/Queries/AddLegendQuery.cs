using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record AddLegendQuery(string Name, int FirstWeaponId, int SecondWeaponId)
    : IRequest<int>;