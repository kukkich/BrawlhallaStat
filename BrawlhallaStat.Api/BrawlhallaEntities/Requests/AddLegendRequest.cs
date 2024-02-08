using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public record AddLegendRequest(int Id, string Name, int FirstWeaponId, int SecondWeaponId) : IRequest;