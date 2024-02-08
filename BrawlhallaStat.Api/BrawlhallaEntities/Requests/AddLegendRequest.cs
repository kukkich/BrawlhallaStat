using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public record AddLegendRequest(string Name, int FirstWeaponId, int SecondWeaponId) : IRequest<int>;