using BrawlhallaStat.Api.Replays.Services;
using MediatR;

namespace BrawlhallaStat.Api.Replays.Commands;

public class UploadReplayHandler : IRequestHandler<UploadReplayCommand, string>
{
    private readonly IReplayService _replayService;
    private readonly ILogger<UploadReplayHandler> _logger;

    public UploadReplayHandler(
        IReplayService replayService,
        ILogger<UploadReplayHandler> logger
        )
    {
        _replayService = replayService;
        _logger = logger;
    }

    public async Task<string> Handle(UploadReplayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _replayService.Upload(request.User, request.File);
            _logger.LogInformation("Replay was saved");
        }
        catch (Exception exception)
        {
            _logger.LogWarning("Error processing the replay: {Message}", exception.Message);
            throw;
        }
    }
}