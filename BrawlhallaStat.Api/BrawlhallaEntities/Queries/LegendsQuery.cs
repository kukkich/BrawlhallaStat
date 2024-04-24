using BrawlhallaStat.Api.Contracts.GameEntities;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record LegendsQuery : IRequest<List<LegendDto>>;