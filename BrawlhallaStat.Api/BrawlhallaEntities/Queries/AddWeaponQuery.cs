using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record AddWeaponQuery(string Name) : IRequest<int>;