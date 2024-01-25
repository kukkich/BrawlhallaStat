using BrawlhallaStat.Api.Replays.Services;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.Replays.Commands;

public class UploadReplayHandler : IRequestHandler<UploadReplayCommand, string>
{
    private readonly IReplayService _replayService;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<UploadReplayHandler> _logger;

    public UploadReplayHandler(
        IReplayService replayService,
        BrawlhallaStatContext dbContext,
        ILogger<UploadReplayHandler> logger
        )
    {
        _replayService = replayService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> Handle(UploadReplayCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var game = await _replayService.Upload(request.User, request.File);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation(
                "Replay was saved. Author {Author}, details {Details}, file {File}",
                game.AuthorId, game.GameDetailsId, game.ReplayFileId
            );

            return game.GameDetailsId;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            _logger.LogWarning("Error processing the replay: {Message}", exception.Message);

            throw;
        }
    }
}