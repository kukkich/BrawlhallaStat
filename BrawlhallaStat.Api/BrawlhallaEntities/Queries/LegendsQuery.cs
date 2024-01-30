using BrawlhallaStat.Domain;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record LegendsQuery() : IRequest<List<Legend>>;