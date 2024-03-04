using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class LegendsQueryHandler : IRequestHandler<LegendsQuery, List<LegendDto>>
{
    private readonly IBrawlhallaEntitiesService _entitiesService;
    private readonly IMapper _mapper;
    private readonly ILogger<LegendsQueryHandler> _logger;

    public LegendsQueryHandler(
        IBrawlhallaEntitiesService entitiesService, 
        IMapper mapper, 
        ILogger<LegendsQueryHandler> logger
    )
    {
        _entitiesService = entitiesService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LegendDto>> Handle(LegendsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Legend query executing");
        var legends = await _entitiesService.GetLegends();
        var result = _mapper.Map<List<LegendDto>>(legends);
        return result;
    }
}