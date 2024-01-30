using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Statistics.Services;

public interface IStatisticService
{
    public Task<Statistic> GetStatisticsAsync(StatisticGeneralFilter filter, IUserIdentity user);
}