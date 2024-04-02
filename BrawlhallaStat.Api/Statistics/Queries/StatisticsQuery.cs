using BrawlhallaStat.Api.General.Paging;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics.Dtos;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Queries;

public record StatisticsQuery(IUserIdentity User)
    : IRequest<IEnumerable<StatisticWithFilterDto>>;

public record StatisticsPagedQuery(IUserIdentity User, Page Page)
    : IRequest<PagedStatisticWithFilterDto>;
