using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Statistics.Services;

public interface IStatisticService
{
    public Task<Statistic> GetStatisticsAsync(StatisticGeneralFilter filter);
}