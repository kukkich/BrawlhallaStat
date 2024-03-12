using AutoMapper;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Dtos;

namespace BrawlhallaStat.Api.Statistics.MapperProfiles;

public class FiltersProfile : Profile
{
    public FiltersProfile()
    {
        CreateMap<StatisticFilterCreateDto, StatisticFilter>();
        CreateMap<StatisticFilter, StatisticFilterPublicDto>();
    }
}