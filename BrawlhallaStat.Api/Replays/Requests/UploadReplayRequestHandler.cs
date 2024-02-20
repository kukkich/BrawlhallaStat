using BrawlhallaStat.Api.Replays.Services;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Replays.Requests;

public class UploadReplayRequestHandler : IRequestHandler<UploadReplayRequest, string>
{
    private readonly IReplayService _replayService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<UploadReplayRequestHandler> _logger;

    public UploadReplayRequestHandler(
        IReplayService replayService,
        BrawlhallaStatContext dbContext,
        ILogger<UploadReplayRequestHandler> logger
        )
    {
        _replayService = replayService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> Handle(UploadReplayRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var game = await _replayService.Upload(request.User, request.File);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "Replay was saved. Author {Author}, details {Details}, file {File}",
                game.AuthorId, game.DetailId, game.ReplayFileId
            );

            return game.DetailId;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning("Error processing the replay: {Message}", exception.Message);

            throw;
        }
    }
}