using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;

namespace BrawlhallaStat.Api.Statistics.Services;

public interface IStatisticService
{
    public Task<Statistic> GetStatistic(StatisticFilterCreateDto filterCreate, IUserIdentity user);
    public Task<IEnumerable<StatisticWithFilter>> GetStatisticsFromUserFilters(IUserIdentity user);
    public Task<StatisticWithFilter> AddFilter(StatisticFilterCreateDto filter, IUserIdentity user);
    public Task DeleteFilter(string id, IUserIdentity user);
}