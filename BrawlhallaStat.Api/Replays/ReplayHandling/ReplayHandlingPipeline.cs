using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Replays.ReplayHandling;

public class ReplayHandlingPipeline
{
    private readonly IEnumerable<IReplayHandler> _pipeline;

    public ReplayHandlingPipeline(IEnumerable<IReplayHandler> pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task Invoke(IUserIdentity author, GameDetail gameDetail)
    {
        var context = new ReplayHandlingContext
        {
            ReplayAuthor = author,
            GameDetail = gameDetail
        };

        foreach (var handler in _pipeline)
        {
            await handler.HandleAsync(context);
        }
    }
}