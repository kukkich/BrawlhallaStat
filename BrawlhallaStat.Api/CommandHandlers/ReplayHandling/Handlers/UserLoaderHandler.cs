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

        var legendId = context.Legend.Id;
        var weaponIds = context.Weapons.Select(x => x.Id).ToArray();

        var opponentLegendIds = context.OpponentLegends.Select(x => x.Id).ToArray();
        var opponentWeaponIds = context.OpponentWeapons.Select(x => x.Id).ToArray();

        var userId = context.ReplayAuthor.Id;

        //TODO there exception cause dbContext already disposed
        context.UserFromDb = await _dbContext.Users
            .Include(x => x.TotalStatistic)
            .Include(x => x.WeaponStatistics
                .Where(ws => weaponIds.Contains(ws.WeaponId)))
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendStatistics
                .Where(ls => ls.LegendId == legendId))
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstLegendStatistics
                .Where(ll => ll.LegendId == legendId &&
                             opponentLegendIds.Contains(ll.OpponentLegendId)))
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstWeaponStatistics
                .Where(lw => lw.LegendId == legendId &&
                          opponentWeaponIds.Contains(lw.OpponentWeaponId)))
                .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstWeaponStatistics
                .Where(ww => weaponIds.Contains(ww.WeaponId) &&
                             opponentWeaponIds.Contains(ww.OpponentWeaponId)))
                .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstLegendStatistics
                .Where(wl => weaponIds.Contains(wl.WeaponId) &&
                             opponentLegendIds.Contains(wl.OpponentLegendId)))
                .ThenInclude(x => x.Statistic)
            .FirstAsync(x => x.Id == userId);
    }
}