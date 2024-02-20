using BrawlhallaStat.Domain.GameEntities;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record WeaponsQuery : IRequest<List<Weapon>>;