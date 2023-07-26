using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Game;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling;

public class ReplayHandlingPipeline
{
    private readonly IEnumerable<IReplayHandler> _pipeline;

    public ReplayHandlingPipeline(IEnumerable<IReplayHandler> pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task Invoke(IUserIdentity author, Game game)
    {
        var context = new ReplayHandlingContext
        {
            ReplayAuthor = author,
            Game = game
        };

        foreach (var handler in _pipeline)
        {
            await handler.HandleAsync(context);
        }
    }
}