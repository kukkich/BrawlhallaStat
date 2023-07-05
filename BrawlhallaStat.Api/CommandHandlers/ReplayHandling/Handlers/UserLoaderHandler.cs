using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class UserLoaderHandler : IReplayHandler
{
    private readonly BrawlhallaStatContext _dbContext;

    public UserLoaderHandler(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(ReplayHandlingContext context)
    {
        var user = await _dbContext.Users
            .Include(x => x.TotalStatistic)
            .Include(x => x.WeaponStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendStatistics)
            .ThenInclude(x => x.Statistic)
            .FirstAsync(x => x.Id == context.ReplayAuthor.Id);
        //TODO there exception cause dbContext already disposed
        context.UserFromDb = await _dbContext.Users
            .Include(x => x.TotalStatistic)
            .Include(x => x.WeaponStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstLegendStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstWeaponStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstWeaponStatistics)
            .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstLegendStatistics)
            .ThenInclude(x => x.Statistic)
            .FirstAsync(x => x.Id == context.ReplayAuthor.Id);
    }
}