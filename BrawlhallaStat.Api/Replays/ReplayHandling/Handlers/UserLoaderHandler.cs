using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Replays.ReplayHandling.Handlers;

public class UserLoaderHandler : IReplayHandler
{
    private readonly BrawlhallaStatContext _dbContext;

    public UserLoaderHandler(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(ReplayHandlingContext context)
    {
        var userId = context.ReplayAuthor.Id;

        context.UserFromDb = await _dbContext.Users
            .FirstAsync(x => x.Id == userId);
    }
}