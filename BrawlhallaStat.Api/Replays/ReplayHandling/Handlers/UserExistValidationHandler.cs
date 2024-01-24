using BrawlhallaStat.Api.Replays.Exceptions;

namespace BrawlhallaStat.Api.Replays.ReplayHandling.Handlers;

public class UserExistValidationHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var userFromGame = context.GameDetail.Players
            .FirstOrDefault(x => x.NickName == context.ReplayAuthor.NickName);

        if (userFromGame is null)
        {
            throw new NoUserInGameException(context.ReplayAuthor.NickName);
        }

        context.UserFromGame = userFromGame;

        return Task.CompletedTask;
    }
}