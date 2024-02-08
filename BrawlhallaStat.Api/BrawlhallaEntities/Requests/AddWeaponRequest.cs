using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public record AddWeaponRequest(string Name) : IRequest<int>;