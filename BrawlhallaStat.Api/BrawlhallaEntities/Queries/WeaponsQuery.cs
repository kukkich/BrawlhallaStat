using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record WeaponsQuery : IRequest<List<Weapon>>;