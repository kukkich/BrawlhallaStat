using AutoMapper;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Statistics.Dto;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Dto;

namespace BrawlhallaStat.Api.MapperProfiles.Dto;

public class StatisticsProfile : Profile
{
    public StatisticsProfile()
    {
        CreateMap<LegendStatistic, LegendStatisticDto>();
        
        CreateMap<Legend, LegendDto>();
        CreateMap<Weapon, WeaponDto>();
     
        CreateMap<Statistic, StatisticDto>();
    }
}