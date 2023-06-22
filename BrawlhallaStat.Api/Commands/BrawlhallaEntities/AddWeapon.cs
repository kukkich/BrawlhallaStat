using MediatR;

namespace BrawlhallaStat.Api.Commands.BrawlhallaEntities;

public record AddWeapon(string Name) : IRequest<int>;