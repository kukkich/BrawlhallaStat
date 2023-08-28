using BrawlhallaStat.Domain.Dto;

namespace BrawlhallaStat.Domain.Statistics.Dto;

public class LegendStatisticDto
{
    public string Id { get; set; } = null!;

    public LegendDto Legend { get; set; } = null!;

    public StatisticDto Statistic { get; set; } = null!;
}