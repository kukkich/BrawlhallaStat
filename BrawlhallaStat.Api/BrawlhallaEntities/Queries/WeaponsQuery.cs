using BrawlhallaStat.Api.Contracts.GameEntities;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record WeaponsQuery : IRequest<List<WeaponDto>>;