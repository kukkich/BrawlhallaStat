using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Services;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public class AddLegendRequestHandler : IRequestHandler<AddLegendRequest>
{
    private readonly IMapper _mapper;
    private readonly IBrawlhallaEntitiesService _entitiesService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<AddLegendRequestHandler> _logger;

    public AddLegendRequestHandler(
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

    public async Task Handle(AddLegendRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _logger.LogInformation(
                "Legend add {Id} {Name} {FirstWeaponId} {SecondWeaponId} transaction begin",
                request.Id, request.Name, request.FirstWeaponId, request.SecondWeaponId
            );

            await _entitiesService.AddLegend(_mapper.Map<AddLegendDto>(request));

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Legend add transaction commit");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning(
                "Legend add transaction rollback: {Message}",
                exception.Message
            );
            throw;
        }

    }
}