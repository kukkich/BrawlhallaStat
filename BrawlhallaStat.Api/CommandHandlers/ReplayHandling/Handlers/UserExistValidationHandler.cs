using BrawlhallaStat.Api.Exceptions.ReplayHandling;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class UserExistValidationHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var userFromGame = context.Game.Players
            .FirstOrDefault(x => x.NickName == context.ReplayAuthor.NickName);
        
        if (userFromGame is null)
        {
            throw new NoUserInGameException(context.ReplayAuthor.NickName);
        }

        context.UserFromGame = userFromGame;

        return Task.CompletedTask;
    }
}