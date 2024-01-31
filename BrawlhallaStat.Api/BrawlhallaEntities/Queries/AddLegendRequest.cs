using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record AddLegendRequest(string Name, int FirstWeaponId, int SecondWeaponId) : IRequest<int>;