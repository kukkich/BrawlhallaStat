using BrawlhallaStat.Domain.Games.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record LegendsQuery() : IRequest<List<LegendDto>>;