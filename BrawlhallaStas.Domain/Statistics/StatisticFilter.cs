using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Statistics;

public class StatisticFilter : StatisticFilterBase, IHaveId<string>
{
    public string Id { get; set; } = null!;
}