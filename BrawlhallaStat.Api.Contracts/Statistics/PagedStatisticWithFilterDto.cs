namespace BrawlhallaStat.Api.Contracts.Statistics;

public class PagedStatisticWithFilterDto
{
    public IEnumerable<StatisticWithFilterDto> StatisticWithFilter { get; set; } = null!;
    public int Total { get; set; }
}