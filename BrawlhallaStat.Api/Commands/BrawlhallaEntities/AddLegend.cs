using MediatR;

namespace BrawlhallaStat.Api.Commands.BrawlhallaEntities;

public record AddLegend(string Name, int FirstWeaponId, int SecondWeaponId) 
    : IRequest<int>;