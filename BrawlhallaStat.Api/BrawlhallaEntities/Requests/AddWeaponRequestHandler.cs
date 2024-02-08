using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public class AddWeaponRequestHandler : IRequestHandler<AddWeaponRequest>
{
    private readonly IMapper _mapper;
    private readonly IBrawlhallaEntitiesService _entitiesService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<AddLegendRequestHandler> _logger;

    public AddWeaponRequestHandler(
        IMapper mapper,
        IBrawlhallaEntitiesService entitiesService,
        BrawlhallaStatContext dbContext,
        ILogger<AddLegendRequestHandler> logger
    )
    {
        _mapper = mapper;
        _entitiesService = entitiesService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(AddWeaponRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Weapon add {Id} {Name} transaction begin",
                request.Id, request.Name
            );

            await _entitiesService.AddLegend(_mapper.Map<LegendDto>(request));

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Weapon add transaction commit");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "Weapon add transaction rollback. Message: {Message}",
                exception.Message
            );
            throw;
        }
    }
}