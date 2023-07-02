using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class LoadDataFromDbHandler : IReplayHandler
{
    private readonly BrawlhallaStatContext _dbContext;

    public LoadDataFromDbHandler(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleAsync(ReplayHandlingContext context)
    {
        int legendId = context.UserFromGame.LegendDetails.LegendId;
        int[] opponentLegendIds = context.Game.Players
            .Where(x => x.Team != context.UserFromGame.Team)
            .Select(x => x.LegendDetails.Legend.Id)
            .ToArray();

        var legendsQuery = from l in _dbContext.Legends
                           where l.Id == legendId || opponentLegendIds.Contains(l.Id)
                           select l;

        var legendsFromQuery = await legendsQuery
            .Include(x => x.FirstWeapon)
            .Include(x => x.SecondWeapon)
            .Distinct()
            .ToArrayAsync();

        var legend = legendsFromQuery.First(x => x.Id == legendId);
        var opponentLegends = legendsFromQuery.Where(x => opponentLegendIds.Contains(x.Id)).ToArray();

        context.Legend = legend;
        context.Weapons = new[] { legend.FirstWeapon, legend.SecondWeapon };

        context.OpponentLegends = opponentLegends;
        context.OpponentWeapons = opponentLegends
            .SelectMany(x => new[] { x.FirstWeapon, x.SecondWeapon })
            .Distinct()
            .ToArray();
    }
}