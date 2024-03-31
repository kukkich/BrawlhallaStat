using BrawlhallaStat.Domain.GameEntities.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public record LegendsQuery : IRequest<List<LegendDto>>;