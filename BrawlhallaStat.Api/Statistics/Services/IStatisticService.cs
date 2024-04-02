using BrawlhallaStat.Api.General.Paging;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;

namespace BrawlhallaStat.Api.Statistics.Services;

public interface IStatisticService
{
    public Task<Statistic> GetStatistic(StatisticFilterCreateDto filter, IUserIdentity user);
    public Task<IEnumerable<StatisticWithFilterDto>> GetStatisticsFromUserFilters(IUserIdentity user);
    public Task<PagedStatisticWithFilterDto> GetStatisticsFromUserFilters(IUserIdentity user, Page page);
    public Task<StatisticWithFilterDto> AddFilter(StatisticFilterCreateDto filter, IUserIdentity actor);
    public Task DeleteFilter(string filterId, IUserIdentity actor);
}