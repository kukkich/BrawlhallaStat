using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record AddWeaponRequest(string Name) : IRequest<int>;