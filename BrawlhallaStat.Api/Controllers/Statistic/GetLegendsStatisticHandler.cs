using AutoMapper;
using AutoMapper.QueryableExtensions;
using BrawlhallaStat.Api.Commands.Statistic;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Statistics.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Controllers.Statistic;

public class GetLegendsStatisticHandler : IRequestHandler<GetLegendsStatistic, List<LegendStatisticDto>>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;

    public GetLegendsStatisticHandler(BrawlhallaStatContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<LegendStatisticDto>> Handle(GetLegendsStatistic request, CancellationToken cancellationToken)
    {
        var userId = request.User.Id;

        List<LegendStatisticDto> legendStatistics = await _dbContext.LegendStatistics
            .Where(s => s.UserId == userId)
            .Include(s => s.Legend)
            .Include(s => s.Legend.FirstWeapon)
            .Include(s => s.Legend.SecondWeapon)
            .Include(s => s.Statistic)
            .ProjectTo<LegendStatisticDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        return legendStatistics;
    }
}