using BrawlhallaStat.Api.CommandHandlers.ReplayHandling.WinLoseCounting;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class SelectWinLoseCounterHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        if (context.UserFromGame.IsWinner)
        {
            context.WinLoseCounter = new WinCounter();
        }
        else
        {
            context.WinLoseCounter = new LoseCounter();
        }

        return Task.CompletedTask;
    }
}