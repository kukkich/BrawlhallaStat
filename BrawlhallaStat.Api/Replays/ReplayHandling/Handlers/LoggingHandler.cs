using BrawlhallaStat.Api.Extensions;
using BrawlhallaStat.Api.Replays.ReplayHandling;
using BrawlhallaStat.Domain;
using System.Text;

namespace BrawlhallaStat.Api.Replays.ReplayHandling.Handlers;

public class LoggingHandler : IReplayHandler
{
    private static readonly string ListItemSeparator = ", ";
    private static readonly string BlockSeparator = "============================";

    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ReplayHandlingContext context)
    {
        _logger.LogInformation(CreateMessage(context));
        return Task.CompletedTask;
    }

    private string CreateMessage(ReplayHandlingContext context)
    {
        var builder = new StringBuilder();
        builder.AppendLine(context.UserFromGame.IsWinner ? "WIN" : "LOSE");
        builder.AppendLine(BlockSeparator);

        builder.AppendLine("--- You ---");
        AddLegend(builder, context.Legend);
        AddWeapons(builder, context.Weapons);

        builder.AppendLine();
        builder.AppendLine(BlockSeparator);

        builder.AppendLine("--- Enemy ---");
        foreach (var legend in context.OpponentLegends)
        {
            AddLegend(builder, legend);
        }
        AddWeapons(builder, context.OpponentWeapons);

        builder.AppendLine();
        builder.Append(BlockSeparator);

        return builder.ToString();
    }

    private void AddWeapons(StringBuilder sb, IEnumerable<Weapon> weapons)
    {
        sb.Append('[');
        foreach (var weapon in weapons)
        {
            sb.Append(weapon.Name);
            sb.Append(ListItemSeparator);
        }
        sb.RemoveLast(ListItemSeparator.Length);
        sb.Append(']');
    }

    private void AddLegend(StringBuilder sb, Legend legend)
    {
        sb.Append($"{legend.Name}: ");
        AddWeapons(sb, new Weapon[]
        {
            legend.FirstWeapon, legend.SecondWeapon
        }); //TODO брать массив из пула, а не создавать новый
        sb.AppendLine();
    }
}