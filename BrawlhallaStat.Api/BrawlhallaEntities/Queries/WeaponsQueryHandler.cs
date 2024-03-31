using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class WeaponsQueryHandler : IRequestHandler<WeaponsQuery, List<WeaponDto>>
{
    private readonly IBrawlhallaEntitiesService _entitiesService;
    private readonly IMapper _mapper;
    private readonly ILogger<WeaponsQueryHandler> _logger;

    public WeaponsQueryHandler(
        IBrawlhallaEntitiesService entitiesService, 
        IMapper mapper,
        ILogger<WeaponsQueryHandler> logger
    )
    {
        _entitiesService = entitiesService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<WeaponDto>> Handle(WeaponsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Weapons query executing");
        var weapons = await _entitiesService.GetWeapons();
        var result = _mapper.Map<List<WeaponDto>>(weapons);
        return result;
    }
}