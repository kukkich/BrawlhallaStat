using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Statistics.Dtos;

public class ExistedStatisticFilterDto : StatisticFilterBase, IHaveId<string>
{
    public string Id { get; set; } = null!;
}
