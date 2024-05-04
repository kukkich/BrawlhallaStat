using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Contracts.Statistics;

public class StatisticFilterPublicDto : StatisticFilterBase, IHaveId<string>
{
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
